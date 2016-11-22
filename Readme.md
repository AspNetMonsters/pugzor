Pugor is the Pug view engine for ASP.NET Core. You might know pug by its previous name, Jade. You can read more about Pug over at their website: https://pugjs.org/api/getting-started.html

Pugzor works by passing your models directly to the node version of pug via Steve Sanderson's glorious JavaScript services. It is actually pretty performant and there are many optimizations still to be made. To use Pugzor you'll need to follow these easy steps

1. Install the pugzor package from nuget
   
   ```
    install-package pugzor
   ```
2. Hook into pugzor in your setup.cs

   ```
      public void ConfigureServices(IServiceCollection services)
      {
            // Add framework services.
            services.AddMvc().AddPugzor();
      }
   ```
   
You can now add .pug files directly to your views directory along side the razor files. The model is available in your pug view directly, the view data and model state are attached as properties to the pug model. 

If your model looked like 

   ```
   {
      "FirstName": "bill"
   }
   ```

   and your view state  like 

   ```
   {
     "Countries" : [ "Canada",...]
   }
   ```

   The result would be 

   ```
   {
      "FirstName": "bill",
      "ViewState": {
            "Countries" : [ "Canada",...]
       } 
   }
   ```

#FAQ

###Is this a joke?

It started off as one but it kind of worked okay so we rolled with it. 

###Could I use it in production

Sure. I mean it is your production site do whatever you want. If you have a bunch of pug views already then this could help you transition to using an ASP.NET Core back end with little difficulty. I don't know that I'd start a whole new site off using it. 

###What's the license?

MIT

###Did anybody actually ask these questions?

No, We're just guessing at what people would ask. Thanks for reminding us how inconsiquential we are. 
