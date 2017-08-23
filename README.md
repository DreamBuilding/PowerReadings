# README #

This console app accepts a path and processes csv files to extract meter values and returns those values 20% outside median values for each type

### Setup ###

* Update the Command Line args to point to a directory where the csv files exist

### Large File Handling ###

* The existing code accepts large files upto the process availble memory.
* The structure is functional for stream processing and supports IEnumerable yielding to support queues instead of memory array is future enhancements.
* Functional handlers could move to pure Azure functions and use segmented nodal table storage with map /reduce hives to provess the median and out of gamut files.

### Contribution guidelines ###

* Author Brett Styles
* Stackoverflow consulted for linq extension for median calculation and modified by Brett Styles
* Test coverage provided over or through core functionality on Microsoft Test Framework
* Code base on NET CORE version 2.0 and NET 4.61 on Visual Studio 2017 15.3
* Copyright Dream Building Pty Ltd and sghared with ERM Power for review

### Who do I talk to? ###

* Brett Styles - brett@dreambuilding.com.au
