#RELEASE NOTES

**Author** Dídimo Grimaldo
**Website*** [link](http://www.coralys.com/)
**Twitter** @coralystech
**Facebook** /Coralys.COM

This software is free to use without any limitation on any (even commercial) code. The authors are however NOT liable
for any disruptions, problems or losses incurred in the use or misuse of this code.

---
### v1.4 12 April 2017
	
	* When switching themes we now remain on the same view. The original threw the user back at Home/Index (v1.4)
	* Simplified way of rigging Global.asax and _ViewStart.cshtml (v1.4)
	* Updated for older projects as .NET Core seems to offer (a somewhat complicated) way to support localization.

### v1.3 3 December 2015

	* Binaries for .NET 4.5 for MVC 5.2/Razor 3.2.3 
	* Fixed issue with null language code
	* Proper fallback of language priority
	* Fallback from specific to generic (en-US to en)

### v1.2 11 August 2015

	* Moved the ThemeSwitcher controller to the DLL rather than deploying the class in the web project. It contains
    code unlikely to be changed and also helps to have it right out of the box, especially if the project where the
	package is deployed has its controllers/models in a DLL separate from the website project.
	* Other changes and fixes

### v1.1 10 August 2015
I took two or three Razor View Engines out there that supported themes and localization but not both at the same time. 
Since I required both features I consolidated the concepts into a single Razor View Engine that is capable of both
themes and localization/internationalization. I also performed several improvements over the separate view engines
that served as a base for my implementation:

	* Created NuGet Package (local use)
	* Updated to current .NET/Razor/MVC versions as the reference projects were quite old and outdated.
	* Original required duplication/multiplication of views to include theme colors because these were defined
    in the view and not on a stylesheet. My version removes the necessity to duplicate views in a Views/Themes/*
	folder hierarchy. Instead it uses the theme name to select a theme stylesheet in the Content/Themes folder.
	* Master files or Layout as well as (partial) views are expected in a limited number of places that are more logical.
	* Visual themes are now in the Content/Themes/ folder where stylesheets (and or images) or a theme are expected.
	* The CSS theme stylesheet is can be either the same name of a theme (Green/Green.css) or standard (Green/Site.css).
	* The theme folder also has a global Site.css that defines look other than colors.
	* The master layouts are now in the Views/Shared/Themes/ folder rather than in the Views/Themes/ folder.
	* In the original a theme may also have a different layout (Views/Shared/Themes/theme/_Layout.cshtml) 
    than the global layout (Views/Shared/_Layout.cshtml).
	* If the theme also has a layout, it overrides the global layout, otherwise the global layout is used.
	* he theme can be defined in the web.config file (system.web/pages[@theme], if not only the session 'theme'
    variable is used.
	* The session theme can be used to override the theme defined in web.config
	* The theme switcher's theme dropDownList now preselects the current theme found in Session/Web.config. This
    is done in ThemeSwitcher/Index.cshtml where I use the extension method DropDownListPreselectByText I created.
	* An AllowEmptyTheme in web.config appSettings allows the user to select None in the theme dropdown so no theme
    stylesheets or layouts are used. If not present in web.config or set to false then any attempt to select None
	in the drop down will actually fallback to the theme selected in Web.config, unless of course it isn't defined
	there either.
	* When compiled with the LOCALIZE symbol (see project properties) localization code is enabled so that
    both Views and Partial Views may also be sought for their localized versions (Index.es.cshtml, _LogOnPartial.nl.cshtml)
	
	
### v1.0 24 June 2014	
	
	* Created from older reference code that provided either themeing OR localization but not both
	* Fixed bugs from original implementations
	* Updated to .NET 4.5 WebPages 3.0 Razor 3.0 Mvc 5.0