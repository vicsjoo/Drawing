# Drawing
Project in C# that attempts to make an efficient, multi-threaded algorithm to generate "fractal" images. Anyone interested can help (and will be kindly appreciated).
<br/>The first challenge to this project was to make the PictureBox update without crashing the entire form, by using a MethodInvoker ( call the method on another thread ).
<br/>Then, after updating the PictureBox to the current bitmap in the memory, there was the need to manually call the method: graphics1.Dispose(); since the garbage collector wasn't doing it's job.
<br/>Right now, refactoring is the top priority.
<br/> After the refactoring and performance tuning, there is the need to create a basic UI, to select from multiple algorithms. 
