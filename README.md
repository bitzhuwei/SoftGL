# SoftGL
Implementation of OpenGL(only a part) in pure C#.
##Applications
Games, 3D Modeling, Visualization, ...
##Initializations: glut, CSharpGL.Windows, SoftGL.Windows, ...
glut, gl.h and Graphics Card Driver works together.
CSharpGL.Windows, CSharpGL and Graphics Card Driver works together.
SoftGL.Windows, CSharpGL and SoftGL works together.
They initialize OpenGL's Render Context and Canvas etc on different OS platforms. 
##Abstract OpenGL: gl.h, CSharpGL, ...
gl.h is declaration of OpenGL API in C.
CSharpGL is definition of OpenGL in C#.
They describe the same thing deep inside. They define what OpenGL has in a blueprint.
##Implementations: Graphics Card Driver, Mesa3D, SoftGL, ...
Implementations of OpenGL API in hardware or software.
The Render Context object(or struct) is created here.