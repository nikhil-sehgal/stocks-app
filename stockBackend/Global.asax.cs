using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.ComponentModel;

namespace stock
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static BackgroundWorker worker = new BackgroundWorker();
        public static bool stopWorker = false;
        public delegate void stocksDelegate();
        public delegate void bitcoinDelegate();
        public delegate void ethereumDelegate();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            worker.DoWork += new DoWorkEventHandler(DoWork);
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(WorkerCompleted);
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Cancelling backdround worker on application closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
            if (worker != null)
                worker.CancelAsync();
        }

        /// <summary>
        /// Making Async calls to refresh functions in Dowork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DoWork(object sender, DoWorkEventArgs e)
        {
            stocksDelegate stocksDelegateObj = new stocksDelegate(RefreshStocks.refreshBSEStock);
            stocksDelegateObj.BeginInvoke(null, null);

            bitcoinDelegate bitcoinDelegateObj = new bitcoinDelegate(RefreshStocks.refreshBitcoin);
            bitcoinDelegateObj.BeginInvoke(null,null);

            ethereumDelegate ethereumDelegateObj = new ethereumDelegate(RefreshStocks.refreshEthereum);
            ethereumDelegateObj.BeginInvoke(null, null);
        }


        /// <summary>
        /// Once the DoWork fn is done, making backdround worker to sleep 30 seconds.
        /// After 30 seconds again DoWork fn will be called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker != null)
            {
                System.Threading.Thread.Sleep(30000);
                worker.RunWorkerAsync();
            }
        }
    }
}
