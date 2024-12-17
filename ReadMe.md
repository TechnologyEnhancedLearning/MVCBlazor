# TODO
- rewrite the blazor components so all use new gb_ blazor components not built in
- refactor components now working well, especially parameter passing
- Complete todos in readme like architecture diagram
- make layout warning unnested for blazor
- top to bottom refactor
- extract the add person to a generic component
- nolonger nhsuk css from issue with the library need to reinstate
- redo buttons rather than static or nojs or interactive, just submit, formsubmit, button maybe
- services still named correctly?



# About
.Net 8 MVC Blazor project, with View Components.

## Good to know
The project has lists as a database and an API for it.
They are in a list of lists to represent some database complexity.
Currently First() is used throughout just for simplicity and only the first list in lists is used.

The routes show the No

## Purpose
- Attempt unified hosted .net 8 webapp application **this was not possible**
	- The project uses MVC Server that serves blazor pages, mvc pages, and mvc pages with blazor components
	- The project uses a client project this enables all the components and pages to be Server or Client side
		- in the case of the full blazor pages they can be full auto
			- (Components cant be interactive auto if served via component tag in MVC)
	- Layout is not shared between MVC and Blazor pages. Its duplicated. In the example it shares NavMenu. Which is 
the bit most likely to be updated.
	- We can have the same routes for blazor and MVC they do not need separate domains
- Explore challenges of adding Blazor to MVC Views
	- No interactive auto but everything else
	- can pass class, list, booles, etc
	- cannot pass complex things like render fragments (there are ways such as making them strings but I don't recommend them)
		- Instead we can make a component specific to the scenario
		- Another more flexible but complex option would involve a more generic component that receives instruction 
on what to create (like a factory, I don't recommend this for now, though there may be scenarios)
		- designing to avoid render fragments where possible
- Attempt to consume and recreate some nhsuk design components **works, nojs support requires prerender and our own base components**
	- radiolist component
		- works noJs
		- works blazor
		- similar html structure could be the same
		- validation works could be further improved to be the same as VC in noJs scenario
		- ViewState can be passed
		- **Moved from shared interface to event passing this may be a choice worth further consideration**
- Ease in adding components **Yes**
	- Components can be added to projects and via service collections when made into packages would automatically be available as long 
		as the package was updated (currently the project uses project references for ease)
- Ease in creating components **Should be ok except for base components**
	- components should be easy to create
	- **however for nojs our own base components will need creating which may be more difficult and very important 
to get right as these are the building blocks for future components.**
- Explore challenges of adding Blazor to MVC and converting View Components
	- Some viewcomponents do not support separate naming of inputs and endpoints or nested models
		- Models being created for use with blazor will need to work for VC as well.
- Create highly separated architecture to enable decision making **Yes**
- Create project such it can be used as a way of trying out blazor **Yes**
	- Has colour coded blazor and mvc pages
	- Has pages for different render modes
	- Has Views
	- Has pages
	- Has data and services with enough complexity to see where shared logic, models may occur
- An architecture that is a starting point for deciding how Blazor will be intergrated and be made sharedable with the design system
	- We don't have packages but we do have references currently for ease
		- There are certain ways of packaging that need exploring and the way css, js (for interacting with none blazor components) has not been explored.
	- We do have a component that is an example of LH specific (Attendees) and one that is Generic (Characters) so 
we can see how both are consumed. Related and at what point the generic gets wrapped into an LH component.

## Quick context on NoJS requirement

Blazor is not designed for NoJS but can be made compatible with it by creating our own versions of basecomponents that work in prerender.
Then future components should only need to provide a few additional parameters such as post routes.
These routes in this project are put on a seperate controller to show they are only required by NoJS. 
And the controllers use the same service the component does so NoJS or interactive the same functionality is used but one uses the controller and one uses the component to do it.

There is also a difference in how state is handled. 
And state is an area that should be examined in detail.
There are options like mediatr package and patterns with an API to centralise state.

In this project the approach is for NoJS the state is the database their is no intermediate state.
This means every change is submitted to a controller which uses a service to hit an api and make the change.
This requires specific controller endpoints just for NoJS, and specific database services and api endpoints just for NoJS.

Components will handle a certain amount of state within themselves.
Then post when they are finished. This means less API calls.
It may also mean we want in future some dispose options for sending state or warnings about losing data if navigating away.
It also means UI differences from NoJS. If a component has a post for every change that goes to the DB in NoJs it will need no final submit button.
But the blazor component handling state itself will need a final submit button for when data is to go to the DB.
This is something we would want a design principle for. As we want our NoJS html to be as much as possible identical to our interactive html.
But we don't want forms in forms.

We need to also decide if we want to keep state in services at all.
With thought to when "circuits" cause new services to be created and how that works with MVC hosted blazor components.
It may be best to have stateless services and API and stateful components, for simplicity.
It seems extremely likely that the services would not be useable in such a way to persist state across components and controllers meaningfully.
There is also added complexity from services existing both in WASM and Server if using Webassembly.

Further reading DLS NoJS requirement: [confluence nojs dls](https://hee-tis.atlassian.net/wiki/spaces/TP/pages/3546611779/Search+sort+filter+and+paginate)

## Explanation of Rendering
Blazor can render Server side via a SignalR connection it makes to update the UI and be interactive.
This work is done on the server. To set up the connection JS is required.

Blazor can render via the browser. This is WASM which is compiling the code to webassembly which the browser uses.
The work is done by the browser. You write no JS but it does require it again for set up.
WASM requires a seperate client project, which means it also is injecting its own version of services to the server.

Blazor can also stream render which is progressively rendering sections of pages. This needs JS and is not useful for us.

Blazor has other options such as Interactive Auto, (we cannot render this way with components provided by the view).
With interactive auto we first render pages serverside which is quicker. Then automatically move to WASM once its uploaded to their browser.

There is also Static, static just puts up the html. Though its static in our case it probably won't be if we have posts built in to out html for 
NoJS requirement.

From these WASM for components renderered in views and interactive auto for pages seems the most desireable.
Components do not know how they are being rendered.

Prerendering is an option for WASM, Server, Interactive Auto. We would want to use prerendering.
Prerendering creates first a static page. This is good for SEO and providing quickly an uninteractive UI initially for the user
rather than waiting for loading. It should exist for a very short time. 

We are interested in prerendering because Blazor can prerender without JS. The prerender is just the static rendering.
Which means if a user has nojs they get the static UI. Which can be interactive via html with posts etc.

In this project we inherit from a base component and inject a service to tell the component if JS is enabled.
If it isn't we will render some components differently. Ideally we should aim for components that are exactly the same
in NoJS and that seems possible as with refactoring it has become increasingly the case, and design decision can be made to support this.
If we don't prerender and a user does not have JS they will see nothing. A way around this is to have the JSIsEnabled in the views and render 
a different component, but this has not been explored as a desireable option here as we dont want to duplicate all top level components though may accept having
some base level NoJS components.



The prerender rendering stage is followed by other lifecycle stages which "hydrate" it with functionality and rerender.
When we provide elements like an edit form with a post, the post will work in the prerender phase giving us our NoJS functionality if there are no further phases.
When the component is hydrated and a service/event is applied to the form this functionality overrides the post.
This means we can avoid having a seperate NoJS element and an interactive element. This approach is what we would hope to acheive we all elements.



Using prerender in this way does introduce some challenges/opportunities.
- If it is a blazor page, we are not passing it a view. Therefore we will not return validation to it, and so to handle more than html validation we must redirect to an MVC page
- Prerender is static, and we are putting in functionality to allow it to operate no js if a user is quick enough
	- they could use the UI nojs functionality (currently we don't track render stage or mode so it will break where 
a NoJS flag is used to provide a different component. But this flag could be changed to track NoJs Prerender 
lifecycle stage or static render mode )
	- for pages if they interact with the prerender and post to an endpoint requiring returning data to the page then they will have to be redirected to an MVC version of the page.
	- prerendering using an injected service will receive the server side version of that service, even if it is rendered WASM and is hydrated with WASM version of a service.
- there is built in a lifecycle render stage with a provided first render flag that is likely to be useful.
- the intention should be for prerender html to be as close to the hydrated version as possible so the screen does not flicker.
- we dont have to prerender everything we can have loaders that wait until lifecycle stages that happen after prerender
- we cant async then apply it to the prerender NoJS anything that would alter it after render require life cycle stages that require JS


## Further Information
The project did have all the view components in and css in previously. It has been refactored out for convenience mostly but did work.
- **TODO** Confluence notes on VC components (in other md currently)
- **TODO** Confluence How to add a component to this project (in other md currently)
- **TODO** Confluence html view options lose in using blazor and how resolved (in other md currently)
- **TODO** Tree shaking (in other md currently)
- **TODO** Error Handling in NoJs and Validation across blazor and vc (in other md currently)
- **TODO** References (in other md currently)
- **TODO** Blazor Pages in NoJS (in other md currently)


# Exploration of Project

## Orientation
### Sites
**TODO** pages useful for what
### Data
**TODO** - so can show architecture would allow to be reuseable
- so can show reuseability services and components

## Design
### Architecture

It is common to have a shared project that both WASM and Server rely on. For us we could bring in all our packages by it a injection collection potentially.
But for now it is only a nicety that could make the project seem more complex. So is not included.

#### Objective of Architecture
- Minimal change to implement components
- Reusability
- Evidence can support all Blazor options
	- Pages
	- WASM
	- Server
	- Auto
	- Streamrendering (This one I dont think is useful to us)
	- Static
- Design intentions
	- Slim WASM
	- Minimal repetition through injection collections
	- Minimal boiler plating in MVC project to add new package component
		- Via injection collections and version number hope to be able to implement new components being available 
without having to do anything except add the component you want to use in the place you want it.


#### Structure

**TODO** - Dependency Diagram, Table explaining why and what each project in solution is for
- Naming should help

#### Naming
In order to easily identify where components come from so we can see how general components are used within LH 
components for example alot of files have a suffix.
So LH_CharactersDBService : IGS_CharactersDBService
Means the implementation is specific for learning hub the but this service is from the general/generic package
so characters simulates a generic component/service of some kind. (Attendees is very similar but represents a purely LH one).

	- LH (learning hub specific generally used for services and entities at times)
	- Package (We are using references but this indicates the project would be a package)
	- GB_ blazor generic
	- LHB learning hub blazor
	- GE_ generic/shared entities
	- GE_VCB view component blazor
	- _I means interactive (want to move away from this)
	- _S means static e.g. nojs (want to move away from this)
	- G_ generic
	- VCB_ viewcomponentblazor
	- P_ .txt placeholder example what might go there but is out of scope
	- GS_ generic services
	- LHS_ learning hub service

## Files what to look at
**TODO** Run script to generate table and what to look at in each

# Project Limitations and Potential Future Additions

## What does it not show
- It is not an example of best practice it is an exploration of what may be worth discussing and evaluating as an approach.
This project is not currently a reference for how to but an example of what can work and may have potential.
- shared layout between Blazor and MVC pages. Blazor has to have its own duplicate of it.
- Safe routing we just whitelist the blazor pages at the moment
- NoJS controller and reroutes is probably not how we would do it, but is illustrative of what blazor does and doesnt need and what is just for nojs


## Desired Future Additions
- use layoutcomponentbase for render pages (ensure the way we are hosting it works) replace gb_pagebase
- Revisit stateservices to include  public event Action AttendeesChanged; subscribe statehaschanged to this
	- all handling occuring in state service
- Exploration of design allowing **ZERO** nojs splits and design principles to support it
	- if there are none then static, prerender pages will just work
	- also investigate option of tracking prerender and static so they are rendered as if NoJS 
		- however if it looks different prerendering like this will show one UI then another
- Refactor buttons can we have a design principle where we have a submit wrapped in an editform for NoJS but useable by blazor too
- Auth headers and auth tokens in blazor see [This patrick god ecommerce repo does have and there is a confluence project for how to set it up](https://github.com/patrickgod/BlazorEcommerce)
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
- revisit IAccessible so it can share it summary all the way up the component chain


## Desired For Different Prototype Or Branch
- packaging (may find package building package options removes need for service collections)
	- scoped css in packages
	- js wrappers being introduced for js triggerable blazor
	- gulp process
	- some resources	
		- [blazor uni component libraries](https://blazor-university.com/component-libraries/)
		- includes getting style sheets [microsoft class libraries](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/class-libraries?view=aspnetcore-8.0&tabs=visual-studio)		
- Mediator state manager
	- revisit mediatr package
- **important** -> prototyping tools

## Car park desired features
- Combining two elements currently handled by separate controllers, so submit-able separately or together
	- Behaviour will be refreshing the blazor components
	- So if one submitted, the second one will refresh on page refresh
		- if we didnt want this we would want blazor storage or Mediator to hold temporary state ... or just the controller to return it without saving it?
	- We need to be careful of forms in forms for MVC
- Improve validator component [try this blazor uni in future](https://blazor-university.com/forms/writing-custom-validation/)
	- Uses fluent validation, which requires JS, so maybe not, attributes are better
	- Refactoring the approach used here to combine viewstate and blazor validation may be better


# Recommendations from project
In no particular order.
- if all buttons are to work NoJS and for ease of not splitting to two implementations of html. Use editform with submit buttons for all buttons. and onsubmit will be overriden by blazor.
This will require buttons to not be used within editforms at all (excluding a forms submit). This way it can be NoJS compatible
- If we want to redirect from blazor pages for nojs use nojs script tag and meta tag to redirect. This is because we 
run the code as we prerender, using 
navigationmanager in prerendering stages will happen 
before render. even if async. which means we cant show 
html like "you have no js your being directed" on our 
blazor pages. Though we can still use the navigation manager to redirect it just can't happen after prerender.
It would be possible to tell them they we're redirected on the page they we're redirected to if this was preferable.
- Elements need to work without JS without the NoJs flag so Prerender and static work too. 
	- Or we need the NoJS flag to also be triggered by prerendering and static.
	- Or we disable static and prerendering it NoJS flag is false so they are truly static
		- NoJs false disable submit buttons and enable on hydration
- Splitting implementation for NoJS should be a last resort
	- can we wrap buttons in editforms
	- can we overide functionality in hydration
- Controllers should use same service as blazor components so posts using same logic
- Reminder Blazor SSR, Server, WASM all but prerender and static use JS in one form or another so there is work required to make 
prerender work without blazor interactivity	
- Remake all default blazor components in our own way
- Blazor does not know what ViewState is so we need pass it. In the project we have an interface for models and a 
custom validation component to handle modelstate validation and Blazors validator component.
- ~~Separate VC model requirements into set of interfaces~~
	- This could work but we would need to put the interfaces on viewmodels, form models, but not just models as we are mixing UI in
	- It requires alot of change
	- This project has moved to passing events without the interface this does mean compatibility is not forced, and we could provide helpers but currently
	on creating the component it needs to be told how to consume the data.
- Warning NoJS requirements require alot of considering and work
	- How components are designed
	- Still needing View controllers to handle functionality
	- Require prerendering (which we may of wanted anyway)
	- More service methods because need to be able to use db as only source of state if needs be
	- Custom base components
		- Lose opportunity for baked in simplicity of some quality of life features like 2 way binding (EditForm with EditContext instead of using the Model results in more complex more manual defining of events rather than leveraging two way binding)
		- Another example passing EditForms a Model is so much easier than editcontext, but we need to do it this way due to custom handling
	- Restriction in Blazor functionality can use, such as quality of life features and more complex approaches that should not be leveraged where we need interactable static renderered html.
- ViewComponents and MVC like flat models for ease Blazor requires less event handling and complexity when no using nested classes because reference changes are harder to track.
- Make all default base components first and as well as we can
	- Then make versions of viewcomponents on them and refactor to make it fit their needs 
- Better base components easier everything else. Also if they require to be changed in the future it may require a lot of testing as all the components that use them may need testing.
	- However we may have unit tests
- Interactive Auto Prerendered Blazor pages and WebAssembly Prerender is probably they way we want to go. As the browser does more work.
- Have a browser set to NoJs for testing components

# Setup **TODO**
- NoJSBaseController ReturnRedirect uses the localhost address so needs changing
- 

# Trouble Shooting
- if assembled files break try deleting them and then starting not rebuilding.
- blazor not detecting any components! - restarted visual studio i was running two instances for comparison
- deleting auto generated files when some issues due to references seems to work even after bin and obj deletion and clean for this kind of error 
	- expect when using packages some of these issues will resolve
- We have an IAccessible interface for our components leveraging C# functionality is an advantage of using blazor so we should see where it can be implemented
	- This project does do this, though it is necessary to chain the parameters down through the components, which means the summaries are lost.
	There is probably a better way of doing this. It may be with cascading. It maybe with inheritance rather than interfaces.

# Things to try in the project to gain familiarity
- replace the li links with a blazor component that takes class the href or two one for mvc and blazor