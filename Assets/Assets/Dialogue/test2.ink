VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
Which branch do you choose?
    + [Branch 1]
        -> chosen1
    + [Branch 2]
        -> chosen2

=== chosen1 ===
You chose branch 1
~scriptEnding = "stop"
-> ending

=== chosen2 ===
You chose branch 2
~scriptEnding = "stop2"
-> ending

== ending ===
* Close
~closeTriggered = true
->END