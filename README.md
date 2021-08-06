# Drawing
Project in C# that attempts to make an efficient, multi-threaded algorithm to generate "fractal" images. The current code is barely legible and almost unmaintanable, but anyone interested can help.
The first challenge to this project was to make the PictureBox update without crashing the entire form, by using a MethodInvoker ( call the method on another thread ).
Then, after updating the PictureBox to the current bitmap in the memory, there was the need to manually call the method: graphics1.Dispose(); since the garbage collector wasn't doing it's job.
Right now, refactoring is the top priority.


