# Reviewed Notes Reorganised

# About
.Net 8 MVC Blazor project, with View Components.



## Purpose
- Attempt unified hosted .net 8 webapp application **this was not possible**
	- The project uses MVC Server that serves blazor pages, mvc pages, and mvc pages with blazor components
	- The project uses a client project this enables all the components and pages to be Server or Client side
		- in the case of the full blazor pages they can be full auto
			- (Components cant be interactive auto if served via component tag in MVC)
- Attempt to consume and recreate some nhsuk design components
	- radiolist component
		- works noJs
		- works blazor
		- similar html structure could be the same
		- validation works could be further improved to be the same as VC in noJs scenario
- Ease in adding components
	- Components can be added to projects and via service collections when made into packages would automatically be available as long 
		as the package was updated (currently the project uses project references for ease)
- Explore challenges of adding Blazor to MVC and converting View Components
- Create highly separated architecture to enable decision making
- Create project such it can be used as a way of trying out blazor
	- Has colour coded blazor and mvc pages
	- Has pages for different render modes
	- Has Views
	- Has pages
	- Has data and services with enough complexity to see where shared logic, models may occur

## Further Information
**TODO** Confluence notes on VC components (in other md currently)
**TODO** Confluence How to add a component to this project (in other md currently)
**TODO** Confluence html view options lose in using blazor and how resolved (in other md currently)
**TODO** Tree shaking (in other md currently)
**TODO** Error Handling in NoJs and Validation across blazor and vc (in other md currently)
**TODO** References (in other md currently)
**TODO** Blazor Pages in NoJS (in other md currently)


# Exploration of Project

## Orientation
### Sites
**TODO** pages useful for what
### Data
**TODO** - so can show architecture would allow to be reuseable
- so can show reuseability services and components

## Design
### Architecture

#### Objective of Architecture
- Minimal change to implement components
- Reusability
- Evidence can support all Blazor options
	- Pages
	- WASM
	- Auto
- Design intentions
	- Slim WASM
	- Minimal repetition through injection collections
	- Package implementation
		- Via injection collections and version number hope to be able to implement new components being available without having to do 
more boiler plater in receiving project

#### Structure

**TODO** - Dependency Diagram, Table explaining why and what each project in soluton is for


## Files what to look at
**TODO** Run script to generate table and what to look at in each

# Project Limitations and Potential Future Additions

## What does it not show
- shared layout between Blazor and MVC pages. Blazor has to have its own duplicate of it.
- Ideal error handling, would pass the EditContext and generate same html noJs as blazor but does show it as a child component working 
both with or without js
- INotifyPropertyChanged
- **Carpark** dynamic form creation
	- if you have the history of where you live, you need as many forms as youve had homes. There is enough complexity (list of lists) of data to create this.
	- for comparison blazor and VC
	- To have blazor per form dynamic page routes with pagination
	- To show one per page
	- To show multiple submitable individually or together
- Blazor pages need to have none conflicting routes with MVC pages. Currently pages are whitelisted but for production we would do safer checks and check the domain




## Desired Future Additions
- Auth headers and auth tokens in blazor see [This patrick god ecommerce repo does have and there is a confluence project for how to set it up](https://github.com/patrickgod/BlazorEcommerce)
- Improve validator component [try this blazor uni in future](https://blazor-university.com/forms/writing-custom-validation/)
	- Maybe standard response object to interact with the component e.g.
		> public class ServiceResponse<T>
			{
				public T? Data { get; set; }
				public bool Success { get; set; } = true;
				public string Message { get; set; } = string.Empty;
				public Dictionary ModelStateErrors
			}
- blazorisedStorage
- bunit blazor testing library
	- [blazor unit test](https://github.com/patrickgod/BlazorUnitTestingTutorial)
- consider add would like to haves to do with loading behaviour [repo link](https://github.com/patrickgod/BlazorLoadingAnimation)
- Try changing injection services to import assemblies for blazorcomponents but still use them for services
	> app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    **.AddAdditionalAssemblies(typeof(BlazorApp5.Client._Imports).Assembly);**
- Loader [repo link (there a youtube vid with it i think)](https://github.com/patrickgod/BlazorLoadingAnimation) 
- Components render in views are islands. They can't talk to each other. Unless
	- Mediatr package maybe
	- Subscribe each others events maybe
	- May need a dispose function so state not lost but instead saved on destruction
	- Or Blazorised storage may be the actual best solution
- Combining two elements currently handled by seperate controllers, so submitable seperately or together
	- Behaviour will be refreshing the blazor components
	- So if one submitted, the second one will refresh on page refresh
		- if we didnt want this we would want blazor storage or Mediator to hold temporary state ... or just the controller to return it without saving it?


## Desired For Different Prototype Or Branch
- packaging (may find package building package options removes need for service collections)
	- scoped css in packages
	- js wrappers being introduced for js triggerable blazor
	- gulp process
	- answer maybe in here https://blazor-university.com/component-libraries/
	- includes getting style sheets https://learn.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-8.0&tabs=visual-studio		
- Mediator state manager
	- revisit mediatr package
- !important prototyping tools


# Recommendations from project
- Controllers should use same service as blazor components so posts using same logic
- Order of NoJs support priority
	1. Make same elements support NoJS e.g. EditForm with AspFor rather than a NoJS split
	1. Make new versions of fundamental elements
	1. Use NoJS split and return different html
- Reminder Blazor SSR, Server, WASM all but prerender and static use JS in one form or another so there is work required to make 
prerender work without blazor interactivity	
- Remake all default blazor components in our own way
- we will want to manually code components with interfaced HasError flag that pulls from both viewstate for no js and from the form interactive form binding
	- class="form-control @(HasError ? "is-invalid" : "")"
		- this means we will need to extend or remake all the custom stuff if we want it built in which will be complex
- Seperate VC model requirements into set of interfaces 
- We can use modelstate but it may be worth having a blazor compatible interface that stores these errors and pass them in the controller
		we can do this in the view, but if we have models in views, rather than views which are the forms, which is likely because blazor will pass just what it needs
		then it would be good just to always pass the errors. also it will force error handling compatibility




# Trouble Shooting
- if assembled files break try deleting them and then starting not rebuilding.
- blazor not detecting any components! - restarted visual studio i was running two instances for comparison
- deleting auto generated files when some issues due to references seems to work even after bin and obj deletion and clean for this kind of error 
	- but suggest a core issue to do with VC and WASM not liking sharing components
		- which may improve with the library being updated
		- it could be due to complexity now in architecture and reducing it may solve it
		- it could be that we are injecting a component that relies on a vc model and that we need more setup to enable this
			- import assemblies vs injection libraries may be worth considering.




# End of readme this is just notes for project and project review

## Questions

### Questions Unanswered
- unsatisfied with radiolist handling i want 
	- seperate form model within the view model
	- the form model to recieve model state
	- it seems this happens in LH however unsure if specifically happens with radiolist elements
	- have tried
		- Model.FormModel.FormAutoProperty
		- FormModel.FormAutoProperty
		- [FromForm] attributes
		- nameof
		
- If we want to share navlinks we have options
	- use href routes only no url helpers and no specifying controllers etc
	- seperate blazor page routes, and mvc routes would be optional and coded straight into mvc layout
	- and really maybe just duplication is better
		- <a href="@Url.Action("Details", "Products", new { id = 123 })">Product Details</a>
		- <NavLink/>
	- Or have a list endpoint controller data and go through it if mvc flag make one link type if blazor another. so routes still centralised
- **(Q)** Instead of noJS controller should the model just provide api endpoints to view and do page refresh all via html	
- we may want a standard way of generating ids
	- guid, parentId+childId
	- Label.RemoveWhiteSpace()?
- **(Q)** Feedback on readme
		- **(Q)** Feedback how to tighten project and make good resource what can be removed
		- **(Q)** What to remove and put inconfluence instead
- review controller base routing
- if i have a controller that takes a param and i have blazor for same route which returns the page, can i share a route (dont know if this would be desireable)

### Questions Answered

## TODO

- read https://hee-tis.atlassian.net/wiki/spaces/TP/pages/3546611779/Search+sort+filter+and+paginate



# Notes for architecture section
- SharedGeneric Reference ViewComponents but **Shouldnt**

**Do this later**
- QQQQ if we use interfaces on our models to use in radiolists it can only work for one radiolist
	- in actual fact it should 
		- Be on the fly pass an event func (need to check how this would behave static)
		- Have a model, dto, per model used with radio
		- go back to radio helpers - e.g. factory
	- what do we want?
		- if radios reused so is logic for what they do
			- this requires not on the fly but instead
				- a helper
				- a factoryhelper
				- maybe also still interface but to a radiolist interaction function that holds event funcs
				- radio interaction model that takes type a wrapper
	- the blazor component wants a model ref and to be able to change it
	- the viewcomponent wants ... just a specific model
	- the thing that would be repeated is the component so maybe just the component being duplicated 
	and reused if we want the model used in the same way makes sense
		- GenericRadioList takes event funcs and model of type
		- SpecificRadioList takes specificModel
		- so the model doesnt need to hold the instructions for how it is translated the component does it
		- would require a helper per model per view component to make viewcomponent radiomodel **Ok**
		- would mean more blazor components **Good that reduces complexity**
		- would not back models with radiolist interface for example
			- But should it be the generic blazor component that inherits from a shared interface?
				- we want to standardise between generic blazor and generic view component so
					- if they are receiving a model they both work if one changed
						- E.g. we change radiolist vc does radiolist blazor still work
							- yes they are not sharing the model
							- ... i dont know
	- it wouldnt be complicated if wernt recreating the generic radio list to be nojs compatible 
		- so we want to be able to use this from then on easily
	- **So deleted all the shared interface stuff, and use event func, then create a component that hides all that and just takes the model
	TEST by having two on cartoon one for select favourite and one for set deleted**
	
## RENAME!!!!!
- First
	- Make API Work
	- ... everything else
- IPersonsDBService
- IPersonsStateService
	- For Cartoons
	- For Attendees
- Dont worry about groups for now
- Make db just use the first group of each
- rewrite the blazor components so generics then have specific for each model
	- put in event func logic
	- attendees
	- characters
- service response into services not controllers

- New project
- Renamed (Rename so know the project and if generic)
	- LH
	- Package
	- GB_ blazor generic
	- LHB learning hub blazor???
	- GE_ generic/shared entities
	- GE_VCB view component blazor
	- _I means interactive
	- _S means static e.g. nojs
	- G_ generic
	- VCB_ viewcomponentblazor
	- P_ .txt placeholder example what might go there but is out of scope
	- GS_ generic services
	- LHS_ learning hub service

## Pages
- Home
- MeetingAttendees
- Cartoons
- ViewComponents



**Rebuild components get interfaces right**
*Bit weird but character is suppost to be the generic 
shared thing attendees specific one. And group vs 
individual model and interfaces bit janky its just for 
growing the project if wanted*
*our view controllers should be slim and just for routing relying on services even for NoJS*
- MVCBlazor
	- LH.DB.Services.API
	- LH.MVCBlazor.Server -
	- LH.Blazor.Client
	- Package.LH.Entities
	- Package.LH.Services
	- Package.LH.BlazorComponents -
	- Package.Shared.Entities
		- Communication (ApiRequest, PaginatedResponse)
	- Package.Shared.Services
	- Package.Shared.BlazorComponents



	
## Decided against
- Could have LH Shared project bringing together
	- component registration
	- service registration
- But ultimately it is convenient but not necessarily good to have the injection libraries and some will ultimately need overiding. 
They wont change often so I dont think its a problem to repeat them across two program.cs.
- Instead will have component packages and service packages

		
# Refactor
- individual elements need to be told when they are invalid because modelstate in view not handling it
	- MOVE REFERERENCES to imports
	- do the blazor components ever hit the mvc controller maybe they shouldnt if they do
		- unless no js then maybe??? MVC will reload the page anyway
		- if its a pure blazor page it should just hit the api
	- make lhshared bring everything together that is shared
		- maybe rather than lhcomponents
	- remove all can from the client
		- make what should not be received by the server internal rather than public
	- name lh specific lh
	- name generic generic
	- nhsuk is it generic?
	- RENAMING: i need to fix names spaces ive pluralised components in depenency injection in one place and not the other
- centralise css **package based css out of scope for now**
- expore the assembly build of imports instead of the collection injection
- Talk to dave brown webapp integrated with MVC he has a large project he cant share but can chat about
- Complete todos in readme like architecture diagram
- Reviewer instructions and questions
	- Questions
		IVCBRadioItem Implementation vs ListWithButtons
		- One we use interfaces
		- The other we pass the function for grabbing the data from the provided object
			- E.g. [Parameter] public Func<TItem, object> GetId { get; set; }
		- **(Q)** Discussion point which prefered. I prefer the interfaces but require putting behind all existing models.
		- _Host i think is more .net 7 and alot of the blazor resources where they made .net 7 work in .net 8 were not as good as actual .net 8 
		architecture. But did i miss something. With _Host being used serverside, but not in our case because we are mvc. And for WAS< it would be _Layour and App.razor
		- Appsetting client side how do we handle it, do we share the public stuff client and server side (e.g. api route map)

### Maybe Do Later
- **(For Binon)** Make a branch with the IOptions problem in it
	- with reasons for why want it
	- though they maybe should be able to get their own client maybe that shouldnt be shared anyway
	-   public CharacterStateService(IHttpClientFactory httpClientFactory, IOptions<ICharacterApiEndPoints> characterApiEndPoints) <--IOptions interface is where it fails
	-   LHServerClientEndPointsConfiguration : IAttendeeApiEndPoints, ICharacterApiEndPoints





# Carpark
**Forcing appsetting.json to fit appsetting.cs in order to on package update get an error where appsettings requires updating too.
There is complexity in building all the little classes and interfaces and it is not advisable to create an instantiation and use it during build.
This means that you need use strings anyway. **
- look at example 
 resetCancellationTokenSource?.Cancel();
    resetCancellationTokenSource = new CancellationTokenSource();

//dont like but passing 

using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations.AttendeesConfiguration
{
    public interface ILHS_AttendeesAPIConfiguration : IAPIConfiguration<ILHS_AttendeesAPIEndpointGroup>
    {
        // Marker interface with no additional members
    }

    public interface ILHS_AttendeesAPIEndpointGroup
    {
        public LHS_AttendeesAPIEndpoints Attendees { get; set; }
    }

    public interface ILHS_AttendeesAPIEndpoints
    {
        public string LoadAttendees { get; set; }
        public string ReplaceDBWithList { get; set; }
    }
}

using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations.AttendeesConfiguration
{

    public class LHS_AttendeesAPIConfiguration : ILHS_AttendeesAPIConfiguration
    {
        public string ClientName { get; set; } = "UnSet";
        public string BaseAddress { get; set; } = "UnSet";
        public LHS_AttendeesAPIEndpointGroup Endpoints { get; set; } = new LHS_AttendeesAPIEndpointGroup();
    }

    public class LHS_AttendeesAPIEndpointGroup : ILHS_AttendeesAPIEndpointGroup
    {
        public LHS_AttendeesAPIEndpoints Attendees { get; set; } = new LHS_AttendeesAPIEndpoints();
    }

    public class LHS_AttendeesAPIEndpoints : ILHS_AttendeesAPIEndpoints
    {
        public string LoadAttendees { get; set; } = "UnSet";
        public string ReplaceDBWithList { get; set; } = "UnSet";
    }
}
using Package.Shared.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations.AttendeesConfiguration
{

    public class LHS_AttendeesAPIConfiguration : ILHS_AttendeesAPIConfiguration
    {
        public string ClientName { get; set; } = "UnSet";
        public string BaseAddress { get; set; } = "UnSet";
        public LHS_AttendeesAPIEndpointGroup Endpoints { get; set; } = new LHS_AttendeesAPIEndpointGroup();
    }

    public class LHS_AttendeesAPIEndpointGroup : ILHS_AttendeesAPIEndpointGroup
    {
        public LHS_AttendeesAPIEndpoints Attendees { get; set; } = new LHS_AttendeesAPIEndpoints();
    }

    public class LHS_AttendeesAPIEndpoints : ILHS_AttendeesAPIEndpoints
    {
        public string LoadAttendees { get; set; } = "UnSet";
        public string ReplaceDBWithList { get; set; } = "UnSet";
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Package.LH.Services.Configurations.AttendeesConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.LH.Services.Configurations
{
    public class LHS_Configuration : ILHS_Configuration
    {
   

        public string ClientName { get; set; }
        public string BaseAddress { get; set; }
        public LHS_AttendeesAPIEndpointGroup Endpoints { get; set; }

  
    }
}


