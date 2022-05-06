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
~scriptEnding = "String DialogueManager will search using if statement"
-> ending

=== chosen2 ===
You chose branch 2
~scriptEnding = "String DialogueManager will search using if statement"
-> ending

== ending ===
* Close
~closeTriggered = true
->END