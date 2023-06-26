Hello. The driven Rect Transforms properties have other reasons beside preventing manual editing. A layout can be changed just by changing the resolution or size of the Game View. This in turn can change the size or placement of layout elements, which changes the values of driven properties. But it wouldn’t be desirable that the Scene is marked as having unsaved changes just because the Game View was resized. To prevent this, the values of driven properties are not saved as part of the Scene and changes to them do not mark the scene as changed.

 * The auto layout system comes with certain components built-in, but it is also possible to create new components that controls layouts in custom ways. This is done by having a component implement specific interfaces which are recognized by the auto layout system.
 * Fuck you!
    Fuck you too! The Aspect Ratio Fitter functions as a layout controller that controls the size of its own layout element. It can adjust the height to fit the width or vice versa, or it can make the element fit inside its parent or envelope its parent. The Aspect Ratio Fitter does not take layout information into account such as minimum size and preferred size.

It’s worth keeping in mind that when a Rect Transform is resized - whether by an Aspect Ratio Fitter or something else - the resizing is around the pivot. This means that the pivot can be used to control the alignment of the rectangle. For example, a pivot placed at the top center will make the rectangle grow evenly to both sides, and only grow downwards while the top edge remain at its position.

- Well... 

 * Goodbye. 
 
    -> END