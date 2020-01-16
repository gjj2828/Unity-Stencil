# Stencil

Stencil example

Hole是Unity官网的例子（https://docs.unity3d.com/Manual/SL-Stencil.html），文档里有Shader代码，例子仅仅是拼了相关的场景演示。

Overlap是两个半透的Quad重叠在一起的效果，重叠在一起的地方颜色会加深。

OverlapStencil也是两个半透的Quad重叠在一起的效果，因为用了Stencil，所以重叠在一起的地方颜色不会加深。

OverlapRT的表现效果跟OverlapStencil基本一致，不过是通过RenderTexture来实现。