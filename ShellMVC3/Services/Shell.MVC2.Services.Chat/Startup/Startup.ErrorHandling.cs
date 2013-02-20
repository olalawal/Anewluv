using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.MVC2.Services.Chat
{
    public partial class Startup
    {
        private static void SetupErrorHandling()
        {
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                try
                {
                    // Write all unobserved exceptions
                    ReportError(e.Exception);
                }
                catch
                {
                    // Swallow!
                }
                finally
                {
                    e.SetObserved();
                }
            };
        }

        private static void ReportError(Exception e)
        {

        }
    }
}
