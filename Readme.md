Pugzor is the Pug view engine for ASP.NET Core. You might know Pug by its previous name, Jade.  
You can read more about Pug over at their website: https://pugjs.org/api/getting-started.html

Pugzor works by passing your models directly to the node version of Pug via Steve Sanderson's glorious [JavaScript Services](https://github.com/aspnet/JavaScriptServices).  
It is actually pretty performant and there are many optimizations still to be made.

# Instructions

1. Install the Pugzor package from NuGet
   
   ```PowerShell
    Install-Package pugzor
   ```
2. Hook into Pugzor in your Startup.cs

   ```csharp
      public void ConfigureServices(IServiceCollection services)
      {
            // Add framework services.
            services.AddMvc().AddPugzor();
      }
   ```
   
You can now add .pug files directly to your Views directory alongside the Razor files.  
The model is available in your Pug view directly, the view data and model state are attached as properties to the pug model. 

If your model looked like this:

   ```json
   {
      "FirstName": "bill"
   }
   ```

   and your view state looked like this:

   ```json
   {
      "Countries" : [ "Canada", "..."]
   }
   ```

   then the result would be:

   ```json
   {
      "FirstName": "bill",
      "ViewState": {
         "Countries" : [ "Canada", "..."]
      } 
   }
   ```

# FAQ

### Is this a joke?

It started off as one but it kind of worked okay so we rolled with it. 

### Could I use it in production?

Sure. I mean it is your production site so do whatever you want. If you have a bunch of Pug views already, then this could help you transition to using an ASP.NET Core back end with little difficulty. I don't know that I'd start a whole new site off using it. 

### What's the license?

MIT

### Did anybody actually ask these questions?

No, we're just guessing at what people would ask. Thanks for reminding us how inconsequential we are. 
