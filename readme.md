# LiveJournal Grabber

A library to download whole posts from LiveJournal. It works by using a custom
XML-based style. 

## Building the solution

1. Clone https://github.com/orloffm/OrlovMikhail.Tools into same-level
directory and build it in Release.
2. Build this solution.

## Preparing LiveJournal

1. Buy a paid LiveJournal account: http://www.livejournal.com/shop/paidaccount.bml.
2. Go to customization page: http://www.livejournal.com/customize/.
3. Search there for `SimpleXML`, switch to that layer. If it is not available,
use its source code from file [src/Data/layer.txt](meta/LiveJournal/Data/layer.txt).
4. Open any post. Press F12 to open the developer console in the browser.
Reload the page. Copy the cookie.

## Working

Now you can download the whole post by doing the following:

	IWorker w = new Worker(...);
	w.Work(...);
	
See the example project `OrlovMikhail.LJ.Grabber.Client` for details.