# About

This document along with the read me is to support feedback and provoke conversation, help identify unknowns and 
opportunities for better approaches.

Please contribute: Add, Edit, Delete its a shared working document.

If this turns out useful we may use it in some form as a proforma for future proof of concept projects.
*Though currently I am not sure what to ask*

## Questions from the POC development process
- is readme missing anything
- what can be removed from readme tldr

### Questions Unanswered
- is this ILHS_AttendeesDbService in the wrong place in the project. API has a reference to LH for its db service but not shared
- NoJS do you agree
	- We should try and have no separate NoJS components and have design process to enable components, and the 
components and pages they belong to, to function without JS.
- Where does state best belong
	- Components
		- state is lost if view refreshed which could happen where Blazor component are mixed with none blazor components
			- There isnt currently an example in the project but a little form would show it
			- An IDisposable implementation may be required
			- It may be we need a design principle or that we wouldnt have MVC that causes refresh sharing a page with a blazor component
			- The solution may be blazorised storage
			- an example is the search is blazor, and has some text in it. A form is posted, everything refreshes. Text is lost.
				- This kind of state loss is more true for blazor because unlike NoJS approaches it will do more before submitting.
		- in our state service we store a list but we also pass one from the view from the same service
			- we can use the list from the view to speed the process
				- then should it update the service created for the component
				- or should the stateservice update the components list and be stateless
		- components in Views are their own circuit. Their state will not be shared with other state service
			- It may be the case two components within a component that is in a view will share that state service
			- So we could have 2 components in a view, and 1 component in a view wrapping two components and different state behaviour
		- Blazorised Storage is commonly used in these scenarios which wont be supported NoJS
		- Mediatr pattern with an api handling all state across the application
				

	- DB (lots of calls and not leveraging blazor interactivity)
- Does it seem that the requirements for Blazor and NoJS will result in too much requirements for
	- forms
	- specific components due to restrictions of rendering components via the view
- Is it worth exploring components that build their render fragments based on received instructions to enable more 
generic top level components and less specific for MVC components.
- IS there a mistake with models for View Component radiolist. I am unsatisfied with radiolist handling i want 
	- separate form model within the view model
	- the form model to receive model state
	- it seems this happens in LH however unsure if specifically happens with radiolist elements
	- have tried to pass the information to the controller with the controller receiving
		- Model.FormModel.FormAutoProperty
		- FormModel.FormAutoProperty
		- [FromForm] attributes
		- nameof
	- I cannot change name, it is set by AspFor in this component.
- Should navlinks be shared between the MVC and Blazor layout
	- We have to use href routes only no url helpers and no specifying controllers
- Buttons
	- should we 
		- put all but submit buttons are wrapped in forms
		- never put buttons except submit buttons in forms
		- have all buttons NoJS working
	- Or
		- are there times we would would want an anchor link instead
- search comment "// Logic here to handle model state for noJS should it be ???" unsure if logic should be elsewhere	

- Feedback on readme
		- Feedback how to tighten project and make good resource what can be removed
		- What to remove and put in confluence instead
		- What was useful and what wasnt?


### Minor questions
- ReassignListGroupPeopleIds<GE_CharacterModel>(Cartoons.Cast<GE_GroupBase<GE_CharacterModel>>().ToList()); wanted not to use a cast here
- //Want to pass a configuration that is a configuration just of what is relevant to the package. And interface to error in the server by not fitting appsettings.
builder.Services.LHS_AddConfiguration(builder.Configuration, "APIs:LH_DB_API"); Tried various approaches with IOptions
-   public CharactersViewModel(List<GE_CharacterModel> characters, GE_FavouriteCharacterFormModel CurrentFormData = null) 
here we do logic in the view. this setup is needed because state is not in the service for incomplete forms from nojs, which is why we are putting in modelstate value


	
# Reflections


	