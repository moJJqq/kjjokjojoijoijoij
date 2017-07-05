# ASP.NET Razor View Engine with Theme and Localization Support
**Author** Lord of Scripts, previous work by others.
---

## SUMMARY

With our enhanced Razor View Engine you can implement MVC web applications with support for visual _themes_
and _localization_ or both at the same time if you wish.

With this add-on you can not only theme and localize your views but also your layouts.

## BACKGROUND
Most sample code and reference projects out there are language centric, meaning that they concentrate solely
in delivering content in the English language. Developers such as me often need to develop websites and 
desktop applications that are multilingual and thus able to deliver content in multiple languages and region
formats.

In my particular situation I was developing a couple of websites, one of which was a tourism website named
Panama Vibes [link](http://www.PanamaVibes.com/) which was about a latinamerican country (Spanish speaking) 
but that also had English content as most of the visitors were from English speaking countries.

Well, By then I was no longer using ASP.NET Website projects (which were nice due to their built-in profile
capabilities) and had migrated my projects to ASP.NET WebForms web applications. The themeing capability was
gone and localization capabilities were rather awkward (to say the least!) for WebForms applications.

On a new iteration of the website I switched to ASP.NET MVC but was still missing both more flexible themeing
as well as localization. There was no way to deliver, out of the box, multilingual content out of a website
with the standard framework.

I started looking for solutions out there and found a couple of outdated (and in some cases buggy) projects
that implemented either Theme support or Localization support as a __Razor View Engine__ which were basically
extensions of the stock view engine.

Unable to find a suitable solution that did both things as per my requirements I concocted a new view engine
that did both themeing and localization based on the working parts of the code I used as reference. 

The original code for localization (I don't remember the author) was cumbersome and required a lot of duplicity
(for bilingual sites) and multiplicity (for multilingual sites) which was going to be a nightmare to maintain
as well as wasting space.

The original code that appeared to support both did not compile anymore and was no longer maintained and also
contained flaws.

I came up with a new design that fixed the drawbacks and made a lot of bug fixes and improvements and updated
the code to current standards.

But nevertheless I wish to give credit to the authors that in one way or another gave birth to this versatile
implementation that I have concocted:

	* "Razor themed view engine for multi-themes site" by Thang Chung from his blog _Context is King_ However,
	  in examining the code with another I quickly realized his code was based a lot on the code mentioned next.
	* "ASP.NET MVC Theme supported razor view engine" by Kazi Manzur Rashid posted on his blog.
	* Thang Chung's modifications were based on work by Chris Pietschmann on a blog post titled "ASP.NET MVC Themed View Engine"

My code is also public domain to follow the spirit of sharing concepts and improvements in order to save others
the pains of implementing the same.

## REQUIREMENTS

Use in your ASP.NET MVC (__Master View Controller__) framework (MVC 5) with the __Razor View Engine__ v3. The
code has been compiled with .NET versions 4.x.

If on the other hand your web application is based on the "new" **.NET Core** you can use the new capabilities of that
framework for localization, though they are somewhat convoluted. But that does not mean you cannot still use this
NuGet package, especially if you want theme support as well.

## INSTALLATION

There are two ways of rigging your ASP.NET MVC projects for using our Enhanced Razor View Engine:

	1. Add a manual reference to OpenSource.Web.Mvc.ThemeableViewEngines.dll, or better yet
	2. Install the NuGet package **AspNet-Mvc-Localizable-Theme-View-Engine**

Then modify your MVC web application as indicated in the section below to add the required support of themes and localization.

## ADAPTING YOUR PROJECT FOR THEMES AND LOCALIZATION

We cannot do magic so you have to modify your project and translate it as well so that our code can do the rest
of the things for you.

### LOCALIZED RESOURCE STRINGS

There are certain things such as simple strings and messages that are better put on __Resource Files__ (.resx) so that
they can be easily reused and corrected without having to do so in every page or view. Explaining resource files is beyond
the scope of this document.

I normally use some prefixes in my resource strings to easily know what they are about and give the suffix a useful name
that makes it easy to know what to expect from the text. You are free to use whatever convention you wish.

	* MSG_ prefix for messages without parameters such as "Welcome to ACME"
	* MSGV_ prefix for messages that include code parameters such as "An {0} Exception ocurred while processing request"
	* STR_ prefix for simple strings such as "Yes" or "Menu" etc.

When Creating resource files (Resources.resx, Resources.nl.resx) in Visual Studio make sure to use the correct attributes by
selecting each of the resource files in the Solution Explorer and then in the Properties window set these properties to:
		
		**CustomToolNamespace:** Translations
		**CustomTool         :** PublicResXFileCodeGenerator

By default the custom tool is just __ResXFileCodeGenerator__ which is not public and will not make your resources visible.
The Custom Tool Namespace you choose to the namespace you want to use to find all your translations. For example if in
Resources.resx you defined a string key named MSG_Welcome with text "Welcome" then in code you would retrieve its 
proper localized value with Translations.Resources.MSG_Welcome.

### LOCALIZED VIEWS (STRUCTURE)

For more extensive localization that is impractical to do with resource files you would use a localized view (Name.CULTURE.cshtml)
with all its text translated to the same culture indicated in the name of the localized view. For more information see below in
this same document.

Localized view files are expected in __the same location of the default view__ following the same notation
used in WebForms, For Example:

	      Views
		     Home
			     Index.cshtml
				 Index.en-UK.cshtml
				 Index.nl.cshtml
				 Index.es.cshtml


### DEPLOYING WEB (VISUAL) THEME STYLESHEETS

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

This you can see in the demo project that has a simple web application with four themes:

	1. Default
	2. Red palette
	3. Green palette
	4. Purple palette

As well as localization of both the views and layouts. In fact in the demo web application you will see that the themes
can even use different layouts, for example a layout without sidebars, with a single sidebar or both. Just let your
creative mind go free because this enhanced view engine will not be an ankle weight limiting your application! 

You can read more about using different layouts for every theme later on this document.

### ESTABLISH DEFAULT CULTURE

**Where:** ~/Web.config  
There is always a primary language for your views, in most cases it is English and then there would be secondary language
views in the languages supported by your web application. So if there is an Index.cshtml view it would have content in
the primary language and then one or more views with content in their respective languages such as Index.es.cshtml (Spanish),
Index.nl.cshtml (Dutch), etc.

Although our code has fallbacks to English in case you forget (or in case it is not your primary language) it would be a good
idea to specify the default cultures for code (region settings such as dates, numbers, etc.) and user interface (the language).
For that you should add the following element as a child of the <system.web> element in Web.config:

	<globalization culture="en-US" uiCulture="en-US" />

In this example we are specifying them as United States English. They are normally both the same but on ocassions you may
wish to have different settings, for example if you do not like the date/number format used by your UI culture you may
want to specify a different setting for the culture. For example I strongly dislike the date setting of US English but also
do not like the number formats used in Europe (commas for decimal digits).

### DEFINE THE DEFAULT THEME ON THE WEB CONFIGURATION

**Where:** ~/Web.config  
In older ASP.NET Website projects you were able to define themes and define the them in the web.config file. However,
in newer versions and web applications that support was dropped. Our view engine supports themes and makes use of that
legacy theme attribute for consistency.

In the system.web section of the site's web.config look the for the <pages> subsection and add the theme 
selection. In this example we are setting the default theme to be _Green_ so make sure the theme files are installed for
that theme as well:

	         <system.web>
			     <pages theme="Green">
				     :
			     </pages>
			</system.web>

### SET IT UP TO USE OUR VIEW ENGINE

**Where:** ~/Global.asax  
If you do not do this the application would still use the stock Razor View Engine, but given that you want both theme
and localization support then you have to modify the __Application_Start()__ method.

After the call to RegisterRoutes() insert this if it is not there already:

			// Instruct Razor to use our enhanced view engine
			OpenSource.Web.Mvc.Controllers.ThemeSwitcherController.RegisterViewEngine(ViewEngines.Engines);
			// Tell the view engine the name of the site cookie where theme and culture overrides will be stored.
            OpenSource.Web.Mvc.Controllers.ThemeSwitcherController.SiteCookieName = "GoodCookie";

In the same file check if there is an **Application_BeginRequest()** method, if it is there make sure that somewhere in it
you call our InitializeCulture() method. Here an example of a basic method:

		protected void Application_BeginRequest(object sender, System.EventArgs e)
		{
			// Let the view engine initialize its culture for the request so that it is localized properly
            InitializeCulture();
		}

### UPDATE THEMED LAYOUTS TO SERVE THE CORRECT THEME STYLESHEET

In your Layout pages (_Layout.cshtml) used in your project that are to be themeable make sure you
have the following in the <html><head> section. That would ensure that your visual theme is rendered properly by all views:

	     <link href="@Url.Content("~/Content/Site.css")" rel="stylesheet"/>
		 <link href="@Url.ThemeStylesheetContent()" rel="stylesheet"/>

 As we mentioned earlier a visual theme may also use a different layout, so you are not forced to have the same
 layout for all themes. If your theme uses the same default layout then do nothing extra, otherwise you have to
 include a **Themes** subfolder under the **~/Views/Shared/** folder to look something like this:

    - If your theme also defines a new layout then do that in the Views folder. Otherwise they use the main
	  layout. For example if the Red theme is going to use a different layout (say 3 columns instead of two)

	      ~/Views
			    Shared
				    _Layout.cshtml            <-- the default layout used by a theme if there is no override
				    Themes
					    Red
						    _Layout.cshtml    <-- overrides the global layout when Red theme is in use

As you can see it in the demo application two of the themes use different layouts whereas the other two use the
standard layout. Just for fun.

### DYNAMIC THEME SWITCHING

In some sites you may want to allow the user to select the session theme which would require the use of a
dropdown list in your view, we show you here how to do it. However, as that really complicates things and
is no longer a trend, we suggest you better set the theme by means of a member profile or something like that,
we would store it in a site cookie for you.

	1. Add the theme switcher partial view/action under the Views folder
	   ~/Views
	      ThemeSwitcher
		     Index.cshtml

	2. Setup your ~/Views/_ViewStart.cshtml
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

## HOW IT WORKS

The webmaster selects a default theme in the web.config (<system.web><pages theme="Green">). When a page
is requested on the browser it first looks for a Session variable containing the selected theme. If there
is one, that theme is used, in other words visitors are able to select a (valid) theme. This functionality
only works if you enable the ThemeSwitcher action in the page.

IF there is no session variable with the theme name we look in web.config for the default theme and use
that. 

The <link> entry in the head of the layout page takes care of enforcing the above behaviour by using the
UrlHelper extension.