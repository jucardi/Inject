using System;

namespace Jucardi.Inject.stereotype
{
    /// <summary>
    /// 
    /// Indicates that an annotated class is a "Repository". Such classes are
    /// considered as candidates for auto-detection when using annotation-based
    /// configuration and classpath scanning.
    /// 
    /// Repository component indicate the purpose of the component is to 
    /// encapsulate storage, retrieval, and search behavior which emulates a
    /// collection of objects.
    /// 
    /// Future releases may come with similar to repository implementations from
    /// the Spring Framework for Java, such as JpaRepository, MongoRepository,
    /// CrudRepository, PagingAndSortingRepository. For now it just acts as a
    /// repository identifier and any data access to data stores must be manually
    /// implemented.
    /// 
    /// <see cref="ComponentAttribute"/>
    /// <see cref="ServiceAttribute"/> 
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RepositoryAttribute : ComponentAttribute
    {
    }
}
