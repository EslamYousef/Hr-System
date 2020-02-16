using System.Web;
using System.Web.Optimization;

namespace HR
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            ScriptBundle newscriptBndl = new ScriptBundle("~/bundles/js");
            newscriptBndl.Include("~/Content/neon/assets/js/select2/select2.min.js",
     "~/Content/neon/assets/js/selectboxit/jquery.selectBoxIt.min.js",
     "~/Content/neon/assets/js/bootstrap-datepicker.js",
     "~/Content/neon/assets/js/bootstrap-timepicker.min.js",
     "~/Content/neon/assets/js/moment.min.js",
     "~/Content/neon/assets/js/daterangepicker/daterangepicker.js",
     "~/Content/neon/assets/js/jquery.multi-select.js",
     "~/Content/neon/assets/js/icheck/icheck.min.js",
     "~/Content/neon/assets/js/gsap/TweenMax.min.js",
     "~/Content/neon/assets/js/jquery-ui/js/jquery-ui-1.10.3.minimal.min.js",
     "~/Content/neon/assets/js/bootstrap.js",
     "~/Content/neon/assets/js/joinable.js",
     "~/Content/neon/assets/js/resizeable.js",
     "~/Content/neon/assets/js/neon-api.js",
     "~/Content/neon/assets/js/jvectormap/jquery-jvectormap-1.2.2.min.js",
     "~/Content/neon/assets/js/jvectormap/jquery-jvectormap-europe-merc-en.js",
     "~/Content/neon/assets/js/jquery.sparkline.min.js",
    "~/Content/neon/assets/js/rickshaw/vendor/d3.v3.js",
     "~/Content/neon/assets/js/rickshaw/rickshaw.min.js",
    "~/Content/neon/assets/js/raphael-min.js",
     "~/Content/neon/assets/js/morris.min.js",
     "~/Content/neon/assets/js/toastr.js",
     "~/Content/neon/assets/js/neon-chat.js",
     "~/Content/neon/assets/js/fullcalendar/fullcalendar.min.js",
     "~/Content/neon/assets/js/neon-calendar.js",
    "~/Content/neon/assets/js/wysihtml5/bootstrap-wysihtml5.js",
    "~/Content/neon/assets/js/fileinput.js",
     "~/Content/neon/assets/js/bootstrap-tagsinput.min.js",
     "~/Content/neon/assets/js/bootstrap-colorpicker.min.js",
     "~/Content/neon/assets/js/datatables/datatables.js",
      "~/Content/neon/assets/js/neon-demo.js",
       "~/Content/neon/assets/js/typeahead.min.js"
                                 );
            bundles.Add(newscriptBndl);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


         //   BundleTable.EnableOptimizations = false;
        }
    }
}


    
 




////
                                  //"~/Content/neon/assets/js/gsap/TweenMax.min.js",
                                  //"~/Content/neon/assets/js/jquery-ui/js/jquery-ui-1.10.3.minimal.min.js",
                                  //"~/Content/neon/assets/js/bootstrap.js",
                                  //"~/Content/neon/assets/js/bootstrap-tagsinput.min.js",
                                  //"~/Content/neon/assets/js/bootstrap-timepicker.min.js",
                                  //"~/Content/neon/assets/js/bootstrap-datepicker.js",
                                  //"~/Content/neon/assets/js/daterangepicker/daterangepicker.js",
                                  //"~/Content/neon/assets/js/jvectormap/jquery-jvectormap-1.2.2.min.js",
                                  //"~/Content/neon/assets/js/selectboxit/jquery.selectBoxIt.min.js",
                                  //"~/Content/neon/assets/js/fullcalendar/fullcalendar.min.js",
                                  //"~/Content/neon/assets/js/bootstrap-colorpicker.min.js",
                                  //"~/Content/neon/assets/js/wysihtml5/bootstrap-wysihtml5.js",
                                  //"~/Content/neon/assets/js/jvectormap/jquery-jvectormap-europe-merc-en.js",
                                  //"~/Content/neon/assets/js/joinable.js",
                                  //"~/Content/neon/assets/js/resizeable.js",
                                  //"~/Content/neon/assets/js/neon-api.js",
                                  //"~/Content/neon/assets/js/typeahead.min.js",
                                  //"~/Content/neon/assets/js/moment.min.js",
                                  //"~/Content/neon/assets/js/jquery.multi-select.js",
                                  //"~/Content/neon/assets/js/icheck/icheck.min.js",
                                  //"~/Content/neon/assets/js/jquery.sparkline.min.js",
                                  //"~/Content/neon/assets/js/rickshaw/vendor/d3.v3.js",
                                  //"~/Content/neon/assets/js/rickshaw/rickshaw.min.js",
                                  //"~/Content/neon/assets/js/datatables/datatables.js",
                                  //"~/Content/neon/assets/js/select2/select2.min.js",
                                  //"~/Content/neon/assets/js/raphael-min.js",
                                  //"~/Content/neon/assets/js/morris.min.js",
                                  //"~/Content/neon/assets/js/toastr.js",
                                  //"~/Content/neon/assets/js/neon-chat.js",
                                  //"~/Content/neon/assets/js/fileinput.js",
                                  //"~/Content/neon/assets/js/neon-calendar.js",
                                  //"~/Content/neon/assets/js/neon-custom.js",
                                  //"~/Content/neon/assets/js/neon-demo.js"