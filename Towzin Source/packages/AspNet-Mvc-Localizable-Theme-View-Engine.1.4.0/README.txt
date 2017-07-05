Maintainer: Lord of Scripts
Available as a NuGet Package (public) on www.nuget.org

Current NuGet package v1.3 (01 Dec 2015)

CREDIT WHERE IT IS DUE
-----------------------
Source originally taken from "Context is King" blog at http://weblogs.asp.net/thangchung/archive/2011/06/10/razor-themed-view-engine-for-multi-themes-site.aspx
titled "Razor themed view engine for multi-themes site" by Thang Chung. It was for an older ASP.NET MVC and didn't compile so I fixed it. I
also addressed the flaws of that solution and enhanced it.

That blog post however was based a lot on the "ASP.NET MVC Theme supported razor view engine" post at
http://kazimanzurrashid.com/posts/asp-dot-net-mvc-theme-supported-razor-view-engine by Kazi Manzur Rashid on his
"Sharing thoughts and learning" blog.

Additionally Thang Chung's modifications were based on work by Chris Pietschmann (http://pietschsoft.com/post/2009/03/ASPNET-MVC-Implement-Theme-Folders-using-a-Custom-ViewEngine.aspx)
blog poast about "ASP.NET MVC: Implement theme folders using a custom view engine" and Christophe Geers blog post titled
"ASP.NET MVC Themed View Engine" post at http://cgeers.com/2010/02/25/asp-net-mvc-themed-view-engine/

However at least two were seriously flawed in the sense that to implement themes it required the Views to also be themed.
So for each theme the view needed or could be duplicated. If it could it created too much freedom which leads to chaos and
maintenance nightmares. If it needed then it defeated the purpose of themes and would get even more complicated when localization
was added to views.

Below are the fixes and improvements done by me over the sources previously mentioned so that they accomodate
in a development framework.

FIXES DONE
  - Original requires duplication/multiplication of views to include theme colors because these were defined
    in the view and not on a stylesheet. My version removes the necessity to duplicate views in a Views/Themes/*
	folder hierarchy. Instead it uses the theme name to select a theme stylesheet in the Content/Themes folder.
  - Now works with MVC 5

IMPROVEMENTS
  - Master files or Layout as well as (partial) views are expected in a limited number of places that are more logical.
  - Visual themes are now in the Content/Themes/ folder where stylesheets (and or images) or a theme are expected.
  - The CSS theme stylesheet is can be either the same name of a theme (Green/Green.css) or standard (Green/Site.css).
  - The theme folder also has a global Site.css that defines look other than colors.
  - The master layouts are now in the Views/Shared/Themes/ folder rather than in the Views/Themes/ folder.
  - In the original a theme may also have a different layout (Views/Shared/Themes/theme/_Layout.cshtml) 
    than the global layout (Views/Shared/_Layout.cshtml). 
  - If the theme also has a layout, it overrides the global layout, otherwise the global layout is used.
  - The theme can be defined in the web.config file (system.web/pages[@theme], if not only the session 'theme'
    variable is used.
  - The session theme can be used to override the theme defined in web.config
  - The theme switcher's theme dropDownList now preselects the current theme found in Session/Web.config. This
    is done in ThemeSwitcher/Index.cshtml where I use the extension method DropDownListPreselectByText I created.
  - An AllowEmptyTheme in web.config appSettings allows the user to select None in the theme dropdown so no theme
    stylesheets or layouts are used. If not present in web.config or set to false then any attempt to select None
	in the drop down will actually fallback to the theme selected in Web.config, unless of course it isn't defined
	there either.
  - When compiled with the LOCALIZE symbol (see project properties) localization code is enabled so that
    both Views and Partial Views may also be sought for their localized versions (Index.es.cshtml, _LogOnPartial.nl.cshtml)
  - Moved the ThemeSwitcher controller to the DLL rather than deploying the class in the web project. It contains
    code unlikely to be changed and also helps to have it right out of the box, especially if the project where the
	package is deployed has its controllers/models in a DLL separate from the website project.
  - When switching themes we now remain on the same view. The original threw the user back at Home/Index (v1.4)
  - Simplified way of rigging Global.asax and _ViewStart.cshtml (v1.4)

TO USE
   - Install the DLL Containing the theme MVC/WebForm engine (or use the NuGet package)
   - Add the AllowEmptyTheme key to web.config (done automatically by the NuGet package)
   - Create the themes hierarchy with the stylesheets for each theme. These styles are supposed to complement or
     override the main (root) Site.css by defining mostly color schemes. Remember there are two default names
	 of stylesheets you can use in the theme, one is Site.css the other the {ThemeName}.css

	 LOCALIZED RESOURCES
	 Localized resources can be used for messages (MSG_), parametized messages (MSGV_) or simple strings (STR_) that
	 are set programmatically either on the code -like error messages- or in the views.

	 When Creating resource files (Resources.resx, Resources.nl.resx) make sure to use the correct attributes by
	 selecting each of the resource files in the Solution Explorer and then in the Properties window set:
		CustomToolNamespace: Translations
		CustomTool         : PublicResXFileCodeGenerator

	By default the custom tool is just ResXFileCodeGenerator which is not public and will not make your resources visible.
	The Custom Tool Namespace you choose to the namespace you want to use to find all your translations. For example if in
	Resources.resx you defined a string key named MSG_Welcome with text "Welcome" then in code you would retrieve its 
	proper localized value with Translations.Resources.MSG_Welcome.


	LOCALIZED VIEWS
	For more extensive localization that is impractical to do with resource files you would use a localized view (Name.CULTURE.cshtml)
	with all its text translated to the same culture indicated in the name of the localized view. For more information see below in
	this same document.

	Localized view files are expected in the same location of the default view following the same notation
	   used in WebForms, For Example:

	      Views
		     Home
			     Index.cshtml
				 Index.en-UK.cshtml
				 Index.nl.cshtml
				 Index.es.cshtml

	ADAPTING YOUR WEB APPLICATION

	Add the following globalization element within the <system.web> element in Web.config to set the default culture codes used in
	your website/web application:

		<globalization culture="en-US" uiCulture="en-US" />

	 In the ~/Global.asax.cs Application_Start() method, after the call to RegisterRoutes() insert this if it is not there already:

			// Instruct Razor to use our enhanced view engine
			OpenSource.Web.Mvc.Controllers.ThemeSwitcherController.RegisterViewEngine(ViewEngines.Engines);
			// Tell the view engine the name of the site cookie where theme and culture overrides will be stored.
            OpenSource.Web.Mvc.Controllers.ThemeSwitcherController.SiteCookieName = "GoodCookie";

	 In the ~/Global.asax.cs check if there is an Application_BeginRequest() method, if it is there make sure that somewhere in it
	 you call our InitializeCulture() method. Here an example of a basic method:

		protected void Application_BeginRequest(object sender, System.EventArgs e)
		{
			// Let the view engine initialize its culture for the request so that it is localized properly
            InitializeCulture();
		}

	HOW TO DEPLOY THEME STYLESHEETS
	This is the folder structure that our view engine expects to locate and use the themes. In our concept the theme is more
	of a change of colors rather than structure, so you would be required to split your stylesheets in a part that defines
	structure and a part that simply defines COLORS and possibly shadows for elements.

	 ~/Content
	     Themes                <-- These are the themes that will be listed in the dropdownlist
		     Red
			    Red.css
			 Green
			    images
				   ...
			    Site.css
			 Purple
			    Purple.css     <-- complements the parent Site.css
          Site.cs              <-- mostly layout CSS elements, no color definitions other than default/basic scheme

UPDATE THEMED LAYOUTS TO SERVE THE CORRECT THEME STYLESHEET
	- In your Layout pages (_Layout.cshtml) used in your project that are to be themeable make sure you
	  have the following in the <html><head> section:
	     <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet"/>
		 <link href="@Url.ThemeStylesheetContent()" rel="stylesheet"/>

    - If your theme also defines a new layout then do that in the Views folder. Otherwise they use the main
	  layout. For example if the Red theme is going to use a different layout (say 3 columns instead of two)

	      ~/Views
			    Shared
				    _Layout.cshtml            <-- the default layout used by a theme if there is no override
				    Themes
					    Red
						    _Layout.cshtml    <-- overrides the global layout when Red theme is in use

	HOW TO SPECIFY THE DEFAULT THEME ON THE WEB CONFIGURATION
    In older ASP.NET Website projects you were able to define themes and define the them in the web.config file. However,
	in newer versions and web applications that support was dropped. Our view engine supports themes and makes use of that
	legacy theme attribute for consistency.

	In the system.web section of the site's web.config look the for the <pages> subsection and add the theme 
	selection:
	         <system.web>
			     <pages theme="Green">
				     :
			     </pages>
			</system.web>
	
	DYNAMIC THEME SWITCHING
	In some sites you may want to allow the user to select the session theme which would require the use of a
	dropdown list in your view, we show you here how to do it. However, as that really complicates things and
	is no longer a trend, we suggest you better set the theme by means of a member profile or something like that,
	we would store it in a site cookie for you.

    - Add the theme switcher partial view/action under the Views folder
	   ~/Views
	      ThemeSwitcher
		     Index.cshtml

	- Setup your ~/Views/_ViewStart.cshtml as
	  If using v1.3 or earlier of this View Engine:

	  @{
			var theme = Session["theme"] as string ?? string.Empty;
   
			if (string.IsNullOrEmpty(theme))
			{
				Layout = "~/Views/Shared/_Layout.cshtml";
			}
			@* DEGT changed search algorithm     *@    
			string themedLayout = HttpContext.Current.Server.MapPath(string.Format("~/Views/Shared/Themes/{0}/_Layout.cshtml", theme));
			if (File.Exists(themedLayout))
			{
				Layout = string.Format("~/Views/Shared/Themes/{0}/_Layout.cshtml", theme);  @* DEGT under Shared folder instead. *@
			}
			else
			{
				Layout = "~/Views/Shared/_Layout.cshtml";
			}
		}

		This is also merged by the NuGet package except you will have to edit because you will see the ViewStart
		ending in just another Layout = that would override the theme part (not what you want).

		Or better yet (v1.4 or later of this View Engine) Just modify your _ViewStart.cshtml to have these two lines:
			@{
				OpenSource.Web.Mvc.Models.RenderingInfoModel model = OpenSource.Web.Mvc.Controllers.ThemeSwitcherController.GetRenderingInfo();
				Layout = model.ChosenLayout;
			}

HOW IT WORKS
   The webmaster selects a default theme in the web.config (<system.web><pages theme="Green">). When a page
   is requested on the browser it first looks for a Session variable containing the selected theme. If there
   is one, that theme is used, in other words visitors are able to select a (valid) theme. This functionality
   only works if you enable the ThemeSwitcher action in the page.

   IF there is no session variable with the theme name we look in web.config for the default theme and use
   that. 

   The <link> entry in the head of the layout page takes care of enforcing the above behaviour by using the
   UrlHelper extension.