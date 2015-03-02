using System.ComponentModel.DataAnnotations.Schema;
using Repository.Pattern.Infrastructure;
using System.Runtime.Serialization;

namespace Repository.Pattern.Ef6
{
    [DataContract]
    public abstract class Entity : IObjectState
    {
        [DataMember]
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}