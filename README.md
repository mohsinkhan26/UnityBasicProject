# Unity Basic Project #

A boilerplate project that many games could take advantage of. As it is a basic Unity project to start with. I used some portions of it in my game projects, now writing it so that it's more usable in projects to come.

Also, It is structured properly with the namespace that it would not conflict with your code at any time.

### Module/Feature list: ###
* **Singleton** - generic singleton which can be used to implement managers
* **MonoDirect** - solution to Monobehaviour function callbacks (called through reflections) which can be slow in most cases and could be the reason of lag
* **SceneController** - which should be used to switch scenes
* **ReportEmail** - email reporting module which allows you to mail console data to given email address
* **GameLogger** - allows you to log with a lot of features like color, timestamp, italics, bold etc.
* **MonoExtension** - includes a lot of extension methods like InvokeExtension, UpdateExtension, AlphaFade etc.
* **ToggleBehaviour** - allows you to enable & disable components, specially use it in your UIs
* **WebAudioClip** - allows you to download audio file from given URL
* **WebData** - allows you to download text data from given URL

### Editor Module/Feature list: ###
* **ButtonExtension** - extended Button component with Text and Animator field added
* **TextExtension** - extended Text component with text Id and Animator field added
* **ToggleExtension** - extended Toggle component to add support for Text and TextExtension to edit text of Toggle, Animator 
* **ToggleInputExtension** - extended Toggle component to add support for InputField, Animator 
* **SliderExtension** - extended Slider component to add support for Label to edit text of Slider, Value and Animator 
* Anchors selected UI to corners
* Find gameObjects which have missing scripts
* Find nested gameObjects which have missing scripts - recursively
* Pivot set

### Usage ###
* Just download project or released plugin version
* put/import it in your project
* And start using the basic listed features
* ***OR*** You can use this project as the starting point of your project
* ***OR*** You can import from Package Manager by selecting the git option with URL using `master-upm` branch [https://github.com/mohsinkhan26/UnityBasicProject.git#master-upm](https://github.com/mohsinkhan26/UnityBasicProject.git#master-upm)

### TODO ###
* A lot to come, stay tuned

### Special Thanks ###
* All the users who point mistakes and reported issues
* All the users who provide feedback and suggestions to improve


## Thanks for your support! ##