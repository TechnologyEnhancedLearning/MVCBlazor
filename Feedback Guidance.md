# About

This document along with the read me is to support feedback and provoke conversation, help identify unknowns and 
opportunities for better approaches.

Please contribute: Add, Edit, Delete its a shared working document.

## Questions from the POC development process


### Questions Unanswered

- NoJS do you agree
	- We should try and have no seperate NoJS components and have design process to enable components, and the 
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
	- Services
	- A Mediator Pattern API of somekind
	- Blazorised Storage (can't work in NoJS)
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

- Feedback on readme
		- Feedback how to tighten project and make good resource what can be removed
		- What to remove and put in confluence instead
		- What was useful and what wasnt?
- review controller base routing it is just for seperation in the project but would like to talk about it.
	- How will parameters and different routing options affect the Blazor Host do we expect a conflict