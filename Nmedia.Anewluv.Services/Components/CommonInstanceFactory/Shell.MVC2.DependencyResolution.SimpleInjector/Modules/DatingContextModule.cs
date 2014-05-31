


using System;
using System.Collections.Generic;
using System.Linq;
using Shell.MVC2.Data;
using Shell.MVC2.Interfaces;
using SimpleInjector;
//to do do away with this when we go to code first , we would pull this from entities 
using Dating.Server.Data.Models;

namespace Shell.MVC2.DependencyResolution.SimpleInjector.Modules
{
	public class DatingContextModule : ISimpleInjectorModule 
	{
        public override void Load(Container container)
		{
            container.Register<AnewLuvFTSEntities, AnewLuvFTSEntities<();
            container.Register<PostalData2Entities>().ToConstructor(x => new PostalData2Entities());
                      
		}
	}
}