VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
Your vision unblurs, dark caverns unveil themselves before you.
    + [...]
        -> next

=== next ===
Your mind is hazy as control returns. 
~scriptEnding = "next"
    + [...]
        -> next1

=== next1 ===
A familiar place with untold secrets.
~scriptEnding = "next1"
    + [...]
        -> next2

=== next2 ===
A pendant sits in your hand, a strange symbol in yellow.
~scriptEnding = "next2"
->ending

== ending ===
* Have a Look around.
~closeTriggered = true
->END