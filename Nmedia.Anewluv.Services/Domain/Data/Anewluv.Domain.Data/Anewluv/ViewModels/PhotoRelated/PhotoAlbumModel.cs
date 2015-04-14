using Nmedia.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Domain.Data.ViewModels
{
    [DataContract]
    public class PhotoAlbumModel
    {
        public int id { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public List<listitem> securitylevels  { get; set; }
       
    }
}
