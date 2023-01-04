# WorldAPI
<p align="center">
  <a href="#"><img src="https://raw.githubusercontent.com/Hacker1254/WorldClient-Files/main/WorldClient.png"></a>
</p>


`I HATE MAKING README's THIS WILL SUCK, BUT DOESN'T REFLECT TO THE API`

# WorldAPI
      
 - QM Support
 - Wing Support (Beta)
 - MainMenu Support (Beta)
 
 Message Me if u need help
 
 if you want something more simple and cleaner, i'd rec using [WTFBlazes Button's API](https://github.com/WTFBlaze/BlazesButtonAPI)

[Im Writng the Docs Later, DM me for Help]


 <details>
<summary>QM</summary>

<details><summary>Buttons</summary><p>
<details><summary>Duo</summary><p>

</p></details>
QM Buttons Has a few uses, its used to open QM Windows, and to Use Functions
here An an Example of it to open a `VRCPage`

`new VRCButton(maiBtngrp, "MenuName", "Open theMenu", () => VRCPAGEVAR.OpenMenu(), false, true, spriteImage);
`
<details><summary>ExtentedControl</summary><p>
ExtentedControl is a ext of the VRCButton, Comming From Root, it Adds a few extra Functions to let u mess with the buttons

List:
```cs
SetToolTip
SetSprite
GetSprite
ShowSubMenuIcon
SetIconColor 
SetAction
SetBackgroundImage // Idea From WTFBlaze
RecolorBackGrn // :3
TurnHalf
CopyToWing // Copys it to the wing Menu (BETA)
```
</p></details>

</p></details>

<details><summary>Toggles</summary><p>

</p></details>
<details><summary>Groups</summary><p>

</p></details>

</details>
<details><summary>Root</summary><p>
This is stuff that can be done on Anything that has the BaseType 

```cs
SetActive
SetTextColor
SetRotation
SetPostion
GetGameObject
GetTransform
ChangeParent
AlsoAddToMM // BETA
AddButton
AddToggle
AddLable
AddGrpOfButtons
AddGrpToggles
AddDuoButtons
```
</p></details>
