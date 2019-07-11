A given file has list of functions with their start time
and stop time.
Their can be multiple fuctions before a stop (with a timestamp) 
occurs.
This stop will be for the most recent function call.
The task is to find the elapsed time of each function.

Input File Format:
start,<funcname>,<timestamp>
start,<funcname>,<timestamp>
stop,<timestamp>
.
.
.

Output File Format:
<funcname> : <elapsed time>
<funcname> : <elapsed time>
.
.
.
