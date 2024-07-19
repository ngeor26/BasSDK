---
parent: FAQ
grand_parent: Guides
---

# Item FAQ

This page is to display frequently asked questions about creating Item and Weapon mods. This page will be updated for new questions that are asked frequently by the community.

{: .tip}
Question not here, or none of the points worked? Ask for help in the #pcvr-modding-help channel and the #nomad-modding-help channel in the [Blade and Sorcery Discord](https://discord.gg/atdUuvd6).

## How do I make items/weapons in Blade and Sorcery?

To make Weapons in Blade and Sorcery, you can follow this video guide [Here](https://youtu.be/-yjZAnniklM) (this video will be updated to 1.0 later. This video still works for 1.0). Ensure that your Unity Version is `2021.3.39f1`. 

You can also follow the guides in the #modding-resources channel in the [Discord](https://discord.gg/atdUuvd6).

## Why doesn't my item spawn?

There are many reasons why an item would fail to spawn when spawned either from the item book or any other method. 
- Make sure that the addressable address of the item in Unity matches the "prefabAddress" in the item json.
- Ensure that the item has all the default components. This includes the HolderPoint, ParryPoint and SpawnPoint
- Ensure your item has colliders which are on the Default/MovingItem layer.
- Ensure that the json is functional/correctly complete. You can check this with the Player.Log or the F5 console. You will be able to see an error that the json could not be read, and will give you the line that failed. 
- Check the mod folder to ensure that the item bundle has been exported correctly. 
- Check the manifest.json generated by Unity in the mod folder to ensure that the version is "1.0.0.0". Also check if your mod has a manifest.json file.
- Ensure that when you build your mod files, the "Export" tickbox is enabled in the Addressable Bundle Builder.
- Ensure that the Item ID in the JSON is unique and not conflicted.
- Ensure that your Unity version is version `2021.3.38f1`
- Ensure that you have set up your labels on the item addressable correctly. They should be "Windows" or "Android" (or both).

## Why doesn't my item do any damage or make any sounds against enemies?

This problem is usually due to the colliders or the damagers set up for this item.
- Check the colliders to ensure that they have the physics colliders correct for them. The best materials for the weapon head (blade) is `Wood` and `Blade`.
- Ensure that the Damager ID setup on the damager part of the JSON references a damager json that actually exists
- Ensure that your item has the damagers set up correctly. You can find the WIKI Page for Damagers [Here][Damager].
- Ensure that the colliders are set up properly, and that all colliders are under a colliderGroup.
- Ensure that all of your collider groups are referenced under the "ColliderGroup" section of the JSON.

## My weapon doesn't feel good to handle

Weapons not feeling great to use could be a few problems you may encounter when creating your weapons/items.
- Check the Mass/Drag/Angular Drag values of the item. This will be located in your item json, if "overwriteMassAndDrag" is set to true. Small items like daggers and small axes can have a lower weight (like 0.8 mass), swords have a medium sized weight (1.0-1.4 mass) and larger weapons like mauls and greatswords can have a higher mass to account for two-handed handling (2.0-8.0 mass). Weapons can have an alternating drag, however for the best feeling, 1.0 drag will suffice for all weapons. 
- Ensure that the colliders created for your item are not too thin, as this could cause them to phase through objects or not cut as effectively. For thin blades and blade edges, capsule colliders are the best for cleaner collision. Remember that more accurate colliders (like using Mesh Colliders) are more expensive than using primative colliders, and it is hardly noticable if done correctly. The more simpler the collider used, the more likely the weapon won't go through objects or interact against enemies effectively, especially on Low Quality Physics like on Android.
- If your weapon is thin, you might want to add an Inertia Tensor Collider. Under your item's "Item" script, you can see a tickbox regarding an Inertia Tensor Collider. When you tick it, it should generate a capsule collider with "trigger" enabled in it. Scale this collider to fit your weapon from top to bottom, making sure to fill your item. 
- If your item is top heavy, you can adjust the center of mass via the Item Script. Move the center of mass to the middle of the heavy area.

## Why can't I grab my weapon?

If you can't grab your weapon, it is usually an issue with handles. Check [here][Handle] to check that you've set up your item correctly.

## Why do enemies who use my weapon parry weirdly?

This is due to the "ParryPoint" being setup incorrectly. Check [here][Item] to look at how the ParryPoint is set up.
 


[Damager]: {{ site.baseurl }}{% link Components/ThunderRoad/Items/Damager.md %}
[Handle]: {{ site.baseurl }}{% link Components/ThunderRoad/Items/Handle.md %}