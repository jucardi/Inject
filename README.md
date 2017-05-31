# Inject - .NET dependency injection.

> *Still under development, first version will be available soon in NuGet.
In the mean time, if you would like to try it, feel free to clone the solution
and compile it and submit any suggestions you may have. All features mentioned
in the README.md are ready to go. Track pending stories here*
https://trello.com/b/RwsNDyao/inject

## Spring-Like dependency injection for .NET Core

.NET library that emulates the Java Spring Frameworks dependency injection
behavior using **Beans** and **Component** annotations.

Unlike Java Spring, **Inject** does not require an `ApplicationContext` or 
container to initialize the dependency injection, but rather an entry point for
the injection to occur.

## Creating beans

**Inject** supports creating beans using `Configuration` classes like
Spring Framework. XML configuration files for beans are not supported at this
time.

#### Configuration classes
Just like in Spring Framework for Java, a configuration class can be created by
simply creating a class and adding the `Configuration` attribute. This will
indicate to the `Scan` process that the class should be loaded to read beans.

E.g.

```csharp
using System;
using Jucardi.Inject.Attributes;

namespace Company.Product.Configuration
{
    [Configuration]
    public class ServiceConfiguration
    {
        [Bean] // if no name given, the method name will be used for the bean name
        public ISomeService ServiceImpl1()
        {
            return new SomeServiceImpl1();
        }

        [Bean(Name = "ServiceImpl2")]
        public ISomeService SomeMethodName()
        {
            return new SomeServiceImpl2();
        }
    }
}
```

In the example above, two beans for the `ISomeService` interface are created,
the first one is named after the method name *(ServiceImpl1)* since no name was
specified to the `Bean` attribute. The second bean however, will be named by
the value specified in the `Bean` annotation *(ServiceImpl2)*

When injecting the dependencies into another class, to specify which bean to
use, simply by adding the `Qualifier` attribute on the field or parameter,
Inject will know which bean to use.

Keep in mind that if multiple beans are available for one type, the `Qualifier`
attribute will be necessary for **inject** to know which dependency to inject,
unless the bean has also the `Primary` attribute.

E.g.

```csharp
using System;
using Jucardi.Inject.Attributes;

namespace Company.Product.Configuration
{
    [Configuration]
    public class ServiceConfiguration
    {
        [Bean]
        [Primary] // Indicates this bean will be used if no Qualifier is specified
        public ISomeService ServiceImpl1()
        {
            return new SomeServiceImpl1();
        }

        [Bean(Name = "ServiceImpl2")]
        public ISomeService SomeMethodName()
        {
            return new SomeServiceImpl2();
        }
    }
}
```

If only one bean exists for a specific type, the `Primary` attribute is not
necessary in the bean definition, nor the `Qualifier` attribute in the
dependency.

##### Injecting dependencies into bean definitions

Just like in Spring Framework for Java, by creating a method in the
configuration class, you may add as parameters as many dependencies as you need,
assuming there are beans defined for these dependencies, and `Inject` will
resolve them. You may use the `Qualifier` annotation for the bean parameters in
the bean method if multiple beans of your dependencies.

```csharp
using System;
using Jucardi.Inject.Attributes;

namespace Company.Product.Configuration
{
    [Configuration]
    public class ServiceConfiguration
    {
        [Bean]
        public ISomeService ServiceImpl1(
                ISomeRepository repository,
                [Qualifier("BeanName")] SomeDependency dependency)
        {
            return new SomeServiceImpl1(repository, dependency);
        }
    }
}
```

## Creating components

Components are simply dependencies that have a single implementation. By adding
any of the component stereotype attributes to the target component class
(`Component`, `Repository`, `Service`), **inject** will know how to create a
single bean for that class so it becomes available as a dependency for injection

E.g.

```csharp
using System;
using Jucardi.Inject.Stereotype;

namespace Company.Product.Services
{
    [Service]
    public class MyService
    {
    }
}
```
This will allow `MyService` to be injected as a dependency into any class.

## Injecting dependencies

#### Constructor based injection

When injecting components as a dependency, if the component has a Constructor
with multiple parameters, **inject** will try to resolve these dependencies
before creating the component instance to be injected. You may use the
`Qualifier` attribute to indicate which bean to inject if multiple beans are
available

E.g.

```csharp
using System;
using Jucardi.Inject.Stereotype;

namespace Company.Product.Services
{
    [Service]
    public class MyService
    {
        private readonly ISomeRepository repository;
        private readonly SomeDependency dependency;

        public MyService(
                ISomeRepository repository,
                [Qualifier("BeanName")]SomeDependency dependency)
        {
            this.repository = repository;
            this.dependency = dependency;
        }
    }
}
```

If multiple constructors are available for the component, exactly one of them
must have the `Autowire` attribute to indicate to that is the constructor to be
used when creating the component instance.

E.g.

```csharp
using System;
using Jucardi.Inject.Stereotype;

namespace Company.Product.Services
{
    [Service]
    public class MyService
    {
        private ISomeRepository repository;
        private SomeDependency dependency;

        [Autowire]
        public MyService(ISomeRepository repository, SomeDependency dependency)
        {
            this.repository = repository;
            this.dependency = dependency;
        }

        public MyService()
        {

        }
    }
}
```

If multiple constructors are available and none or multiple constructors are
attributed the `Autowire` attribute, a `ComponentLoadException` will occur.

#### Field and Property based injection

A Field or Property injection will happen after the instance of the dependency
has been created, **inject** will do a field assignment or a property setter
invoke directly into the bean instance before injecting it as a dependency.

The fields or properties to be injected must be attributed with `Autowire`.
The `Qualifier` attribute may also be used to specify which bean to use.

```csharp
using System;
using Jucardi.Inject.Stereotype;

namespace Company.Product.Services
{
    [Service]
    public class MyService
    {
        // Field injection.
        [Autowire]
        private ISomeRepository repository;

        [Autowire]
        [Qualifier(Name = "BeanName")]
        protected SomeDependency Dependency { get; private set; };
    }
}
```
#### Constructor injection vs field injection

In most cases it is considered a better practice to do a Constructor injection
because it allows the use of the `readonly` keyword, making the dependency
immutable once it has been instantiated.

## Loading beans and components into Inject

#### Scanning specific assemblies

In the start point of the application, by invoking the `Scan` method of `Inject`
specifying the assembly start pattern to scan, Inject will filter all loaded
assemblies into the current domain by the given pattern and scan for
dependencies any assembly that matches the given pattern.

```csharp
class Program
{
    static void Main(string[] args)
    {
        Injector.Scan("Company.Product");
    }
}
```

In this example, all assemblies that start with `Company.Product` will be
scanned for dependencies.

E.g. The following assemblies will be scanned if loaded into the domain.
- Company.Product.Common
- Company.Product.SomeFeature
- Company.Product.SomeOtherFeature

But other assemblies will be ignored.

#### Scanning all loaded assemblies.

Although it is not recommended due to performance and security, Inject also
supports scanning all assemblies loaded into the current domain by not passing
the assembly name pattern into the `Scan` method.

```csharp
class Program
{
    static void Main(string[] args)
    {
        Injector.Scan();
    }
}
```
