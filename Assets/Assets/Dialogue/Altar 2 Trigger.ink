VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
On the wall, an engraving of a vast octopoid being, clad in yellow. 

A strange feeling stirs inside of you.
    + [Try to remember]
        -> remember
    + [Calm your senses]
        -> calm

=== remember ===
Faint memories rush in of a powerful being. Something inside of you moves with reverence. 
~scriptEnding = "remember"
->ending

=== calm ===
Your mind clears as if nothing happened at all. 
~scriptEnding = "calm"
->ending

== ending ===
* Close
~closeTriggered = true
->END