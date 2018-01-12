using System.Web;
using System.Web.Http.ModelBinding;
using System.Web.Optimization;

namespace MetroSupport
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                       "~/Scripts/angular.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/jquerymodal").Include(
                "~/Scripts/jquery.modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymasked").Include(
               "~/Scripts/Custom/jquery.maskedinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/widgetcontroller").Include(
              "~/Scripts/Widget/widget.controller.js",
              "~/Scripts/Widget/widget.models.js"));

            bundles.Add(new ScriptBundle("~/bundles/ItRequestList").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Custom/jquery.datetimepicker.full.js",
               "~/Scripts/ItRequestList/itrequestlistapp.js",
               "~/Scripts/ItRequestList/it.advancesearch.js"));
            bundles.Add(new ScriptBundle("~/bundles/AsppRequestList").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Custom/jquery.datetimepicker.full.js",
               "~/Scripts/AsppRequestList/aspprequestlistapp.js",
               "~/Scripts/AsppRequestList/aspp.advancesearch.js"));
            bundles.Add(new ScriptBundle("~/bundles/PaRequestList").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Custom/jquery.datetimepicker.full.js",
               "~/Scripts/PaRequestList/parequestlistapp.js",
               "~/Scripts/PaRequestList/pa.advancesearch.js"));
            bundles.Add(new ScriptBundle("~/bundles/SvyazRequestList").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Custom/jquery.datetimepicker.full.js",
               "~/Scripts/SvyazRequestList/svyazrequestlistapp.js",
               "~/Scripts/SvyazRequestList/svyaz.advancesearch.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                       "~/Scripts/Custom/jquery.datetimepicker.full.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/itrequest").Include(
                       "~/Scripts/ItRequest/itrequestapp.js",
                       "~/Scripts/ItRequest/it.request.controller.js",
                       "~/Scripts/ItRequest/it.request.models.js"));

            bundles.Add(new ScriptBundle("~/bundles/aspprequest").Include(
                      "~/Scripts/AsppRequest/aspprequestapp.js",
                      "~/Scripts/AsppRequest/aspp.request.controller.js",
                      "~/Scripts/AsppRequest/aspp.request.models.js"));
          
            bundles.Add(new ScriptBundle("~/bundles/parequest").Include(
                     "~/Scripts/PaRequest/parequestapp.js",
                     "~/Scripts/PaRequest/pa.request.controller.js",
                      "~/Scripts/PaRequest/pa.request.models.js"
                     ));
            bundles.Add(new ScriptBundle("~/bundles/svyazrequest").Include(
                     "~/Scripts/SvyazRequest/svyazrequestapp.js",
                     "~/Scripts/SvyazRequest/svyaz.request.controller.js",
                      "~/Scripts/SvyazRequest/svyaz.request.models.js"
                     ));

            bundles.Add(new ScriptBundle("~/bundles/AweFormatGrid").Include(
                    "~/Scripts/AweGrid/formatgrid.js"));
            bundles.Add(new ScriptBundle("~/bundles/assignermodal").Include(
                 "~/Scripts/jquery.modal.js",
                 "~/Scripts/Assigner/assigner.modal.js"
                 ));
            bundles.Add(new ScriptBundle("~/bundles/bossmodal").Include(
                "~/Scripts/jquery.modal.js",
                "~/Scripts/Boss/boss.modal.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/itcategorymodal").Include(
                "~/Scripts/jquery.modal.js",
                "~/Scripts/Category/it.category.modal.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/svyazcategorymodal").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Category/svyaz.category.modal.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/asppcategorymodal").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Category/aspp.category.modal.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/pacategorymodal").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Category/pa.category.modal.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/indexatormodal").Include(
                "~/Scripts/jquery.modal.js",
                "~/Scripts/Indexator/indexator.modal.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/thememodal").Include(
                "~/Scripts/jquery.modal.js",
                "~/Scripts/Theme/theme.modal.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/requestormodal").Include(
                "~/Scripts/jquery.modal.js",
                "~/Scripts/Requestor/requestor.modal.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/locationmodal").Include(
               "~/Scripts/jquery.modal.js",
               "~/Scripts/Location/location.modal.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/devicemodal").Include(
              "~/Scripts/jquery.modal.js",
              "~/Scripts/Device/device.modal.js"
              ));
            bundles.Add(new ScriptBundle("~/bundles/departmentmodal").Include(
           "~/Scripts/jquery.modal.js",
           "~/Scripts/Department/department.modal.js"
           ));

            bundles.Add(new ScriptBundle("~/bundles/changepassword").Include(
          "~/Scripts/jquery.modal.js",
          "~/Scripts/ChangePassword/changepassword.modal.js"
          ));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/LoginOn").Include("~/Content/Login/Login.css"));
            bundles.Add(new StyleBundle("~/Content/Main").Include("~/Content/Main/mainstyle.css"));
            bundles.Add(new StyleBundle("~/Content/Admin").Include("~/Content/Administration/adm.modals.css", "~/Content/Administration/adm.css"));
            bundles.Add(new StyleBundle("~/Content/AssignerModal").Include("~/Content/Administration/Modal/assignermodal.css"));
            bundles.Add(new StyleBundle("~/Content/CatigoryModal").Include("~/Content/Administration/Modal/catigorymodal.css"));
            bundles.Add(new StyleBundle("~/Content/LocationModal").Include("~/Content/Administration/Modal/locationmodal.css"));
            bundles.Add(new StyleBundle("~/Content/RequestorModal").Include("~/Content/Administration/Modal/requestormodal.css"));
            bundles.Add(new StyleBundle("~/Content/BossModal").Include("~/Content/Administration/Modal/bossmodal.css"));
            bundles.Add(new StyleBundle("~/Content/Department").Include("~/Content/Administration/Modal/departmentmodal.css"));
            bundles.Add(new StyleBundle("~/Content/DeviceModal").Include("~/Content/Administration/Modal/devicemodal.css"));
            bundles.Add(new StyleBundle("~/Content/IndexatorModal").Include("~/Content/Administration/Modal/Indexatormodal.css"));
            bundles.Add(new StyleBundle("~/Content/ThemeModal").Include("~/Content/Administration/Modal/thememodal.css"));
            bundles.Add(new StyleBundle("~/Content/Registration").Include("~/Content/UserRegistration/registration.css"));
            bundles.Add(new StyleBundle("~/Content/Modal/ChangePassword").Include("~/Content/jquery.modal.css",
                                                                                  "~/Content/UserRegistration/Modal/changepassword.css"));
            bundles.Add(new StyleBundle("~/Content/ItRequest").Include("~/Content/ItRequest/itrequest.css",
                                                                       "~/Content/ItRequest/it.datetimepicker.css",
                                                                       "~/Content/ItRequest/it.tabs.css"));
            bundles.Add(new StyleBundle("~/Content/ItRequestModals").Include(
                                                                        "~/Content/jquery.modal.css",
                                                                        "~/Content/ItRequest/Modals/it.device.css",
                                                                       "~/Content/ItRequest/Modals/it.assigner.css",
                                                                       "~/Content/ItRequest/Modals/it.catigory.css",
                                                                       "~/Content/ItRequest/Modals/it.devicemodels.css",
                                                                       "~/Content/ItRequest/Modals/it.requestor.css",
                                                                       "~/Content/ItRequest/Modals/it.theme.css",
                                                                       "~/Content/ItRequest/Modals/it.returnto.css",
                                                                       "~/Content/ItRequest/Modals/it.holdon.css"));
            bundles.Add(new StyleBundle("~/Content/PaRequestModals").Include(
                                                                       "~/Content/jquery.modal.css",
                                                                       "~/Content/PaRequest/Modals/pa.device.css",
                                                                      "~/Content/PaRequest/Modals/pa.assigner.css",
                                                                      "~/Content/PaRequest/Modals/pa.catigory.css",
                                                                      "~/Content/PaRequest/Modals/pa.devicemodels.css",
                                                                      "~/Content/PaRequest/Modals/pa.requestor.css",
                                                                      "~/Content/PaRequest/Modals/pa.theme.css",
                                                                      "~/Content/PaRequest/Modals/pa.returnto.css",
                                                                      "~/Content/PaRequest/Modals/pa.holdon.css"));
            bundles.Add(new StyleBundle("~/Content/AsppRequestModals").Include(
                                                                       "~/Content/jquery.modal.css",
                                                                       "~/Content/AsppRequest/Modals/aspp.device.css",
                                                                      "~/Content/AsppRequest/Modals/aspp.assigner.css",
                                                                      "~/Content/AsppRequest/Modals/aspp.devicemodels.css",
                                                                      "~/Content/AsppRequest/Modals/aspp.catigory.css",
                                                                      "~/Content/AsppRequest/Modals/aspp.requestor.css",
                                                                      "~/Content/AsppRequest/Modals/aspp.theme.css",
                                                                       "~/Content/AsppRequest/Modals/aspp.returnto.css",
                                                                       "~/Content/AsppRequest/Modals/aspp.holdon.css"));
            bundles.Add(new StyleBundle("~/Content/SvyazRequestModals").Include(
                                                                      "~/Content/jquery.modal.css",
                                                                     "~/Content/SvyazRequest/Modals/svyaz.assigner.css",
                                                                     "~/Content/SvyazRequest/Modals/svyaz.catigory.css",
                                                                     "~/Content/SvyazRequest/Modals/svyaz.requestor.css",
                                                                     "~/Content/SvyazRequest/Modals/svyaz.theme.css"));
            bundles.Add(new StyleBundle("~/Content/SvyazRequest").Include("~/Content/SvyazRequest/svyazrequest.css",
                                                                       "~/Content/SvyazRequest/svyaz.datetimepicker.css",
                                                                       "~/Content/SvyazRequest/svyaz.tabs.css"));
            bundles.Add(new StyleBundle("~/Content/PaRequest").Include("~/Content/PaRequest/parequest.css",
                                                                       "~/Content/PaRequest/pa.datetimepicker.css",
                                                                       "~/Content/PaRequest/pa.tabs.css"));
            bundles.Add(new StyleBundle("~/Content/AsppRequest").Include("~/Content/AsppRequest/aspprequest.css",
                                                                       "~/Content/AsppRequest/aspp.datetimepicker.css",
                                                                       "~/Content/AsppRequest/aspp.tabs.css"));
            bundles.Add(new StyleBundle("~/Content/ItRequestList").Include("~/Content/ItRequestList/itreqlist.css",
                                                                           "~/Content/ItRequestList/it.datetimepicker.css",
                                                                           "~/Content/ItRequestList/it.advancesearch.css",
                                                                           "~/Content/ItRequestList/it.deleteconfirm.css",
                                                                           "~/Content/ItRequestList/it.report.css",
                                                                           "~/Content/ItRequestList/it.jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/PaRequestList").Include("~/Content/PaRequestList/pareqlist.css",
                                                                           "~/Content/PaRequestList/pa.datetimepicker.css",
                                                                           "~/Content/PaRequestList/pa.advancesearch.css",
                                                                           "~/Content/PaRequestList/pa.deleteconfirm.css",
                                                                           "~/Content/PaRequestList/pa.report.css",
                                                                           "~/Content/PaRequestList/pa.jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/AsppRequestList").Include("~/Content/AsppRequestList/asppreqlist.css",
                                                                            "~/Content/AsppRequestList/aspp.datetimepicker.css",
                                                                            "~/Content/AsppRequestList/aspp.deleteconfirm.css",
                                                                           "~/Content/AsppRequestList/aspp.advancesearch.css",
                                                                           "~/Content/AsppRequestList/aspp.report.css",
                                                                           "~/Content/AsppRequestList/aspp.jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/SvyazRequestList").Include("~/Content/SvyazRequestList/svyazreqlist.css",
                                                                              "~/Content/SvyazRequestList/svyaz.datetimepicker.css",
                                                                              "~/Content/SvyazRequestList/svyaz.deleteconfirm.css",
                                                                              "~/Content/SvyazRequestList/svyaz.advancesearch.css",
                                                                              "~/Content/SvyazRequestList/svyaz.report.css",
                                                                              "~/Content/SvyazRequestList/svyaz.jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/CentralRequestList").Include("~/Content/CentralRequestList/centralreqlist.css",
                                                                            "~/Content/CentralRequestList/datetimepicker.css",
                                                                            "~/Content/CentralRequestList/jquery.modal.css"));
            bundles.Add(new StyleBundle("~/Content/ItServices").Include("~/Content/ItRequestList/it.advancesearch.css",
                                                                            "~/Content/ItRequestList/it.deleteconfirm.css",
                                                                          "~/Content/ItRequestList/it.report.css"));
            bundles.Add(new StyleBundle("~/Content/AsppServices").Include("~/Content/AsppRequestList/aspp.advancesearch.css",
                                                                           "~/Content/AsppRequestList/aspp.report.css"));
            bundles.Add(new StyleBundle("~/Content/PaServices").Include("~/Content/PaRequestList/pa.advancesearch.css",
                                                                         "~/Content/PaRequestList/pa.report.css"));
            bundles.Add(new StyleBundle("~/Content/SvyazServices").Include("~/Content/SvyazRequestList/svyaz.advancesearch.css",
                                                                            "~/Content/SvyazRequestList/svyaz.report.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        //"~/Content/themes/base/jquery.ui.core.css",
                        //"~/Content/themes/base/jquery.ui.resizable.css",
                        //"~/Content/themes/base/jquery.ui.selectable.css",
                        //"~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css"
                        //"~/Content/themes/base/jquery.ui.button.css",
                        //"~/Content/themes/base/jquery.ui.dialog.css",
                        //"~/Content/themes/base/jquery.ui.slider.css",
                        //"~/Content/themes/base/jquery.ui.tabs.css",
                        //"~/Content/themes/base/jquery.ui.datepicker.css",
                        //"~/Content/themes/base/jquery.ui.progressbar.css",
                        /*"~/Content/themes/base/jquery.ui.theme.css"*/));
        }
    }
}