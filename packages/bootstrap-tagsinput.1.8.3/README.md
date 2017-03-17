bootstrap-tagsinput
=============

NuGet Package for **Bootstrap Tags Input**

This is just the distribution files for [Bootstrap Tags Input](https://github.com/timschlechter/bootstrap-tagsinput), and I just packed the sources and assets to work with ASP.NET MVC projects.

## Usage Instructions ##

 - Add a reference to `/Scripts/bootstrap-tagsinput.js`, directly or via bundling: 

**Direct Example**

    <script type="text/javascript" src="/Scripts/bootstrap-tagsinput.js"></script>

**Bundling Example**

At `/App_Start/BundleConfig.cs`:

    public static void RegisterBundles(BundleCollection bundles)
    {
        ...
    
        bundles.Add(new ScriptBundle("~/bundles/general").Include(
                    "~/Scripts/jscolor.js"));

        ...
    }

 - Add a reference to `/Content/bootstrap-tagsinput.css`, directly or via bundling: 

**Direct Example**

    <link href="/Content/bootstrap-tagsinput.css" rel="stylesheet">

**Bundling Example**

At `/App_Start/BundleConfig.cs`:

    public static void RegisterBundles(BundleCollection bundles)
    {
        ...
    
        bundles.Add(new StyleBundle("~/Content/css").Include(
                     ...
                     "~/Content/bootstrap-tagsinput.css",
                     ...
        ));

        ...
    }

You can also use the `min` and/or the `angular` versions.

# License

This project is licensed under [MIT](https://raw.github.com/bootstrap-tagsinput/bootstrap-tagsinput/master/LICENSE "Read more about the MIT license").