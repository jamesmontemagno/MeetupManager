Thanks for using the Coding4Fun Toolkit!

Migration Notes:
2.0.2 -> 2.X
Slider
	Why:  
		Keep in sync with WinStore and WinPhone 8 Slider control
	ToDo:
		Fill property -> Foreground property 
		Step property -> StepFrequency property 
		
1.6 -> 2.X
NameSpace changes:
	Why:  
		Now supporting more than just Phone
	ToDo:  
		Coding4Fun.Phone -> Coding4Fun.Toolkit 

TimeSpanPicker
	Why:
		Removed SL Toolkit dependency, simplifying solution
	ToDo:
		Reference Coding4Fun.Toolkit.Controls DLL instead of Coding4Fun.Phone.Controls.Toolkit

RoundButton, RoundToggleButton, OpacityToggleButton, Tile, ImageTile:
	Why:  
		Add support for vector paths, not just Images
	ToDo:
		Content property -> Label property
		Content now becomes what is in the center, path, text, whatever you want