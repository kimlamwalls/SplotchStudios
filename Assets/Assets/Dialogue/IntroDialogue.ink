VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
Your vision unblurs, dark caverns unveil themselves before you.
    + [...]
        -> text1

=== text1 ===
A familiar place with untold secrets.
    + [...]
        -> text2

=== text2 ===
Your mind is hazy as control returns. 
    + [Have a look around]
        -> text3
    + [Stand there and do nothing]
        -> text3

=== text3 ===
A pendant sits in your hand, a strange symbol in yellow.
~scriptEnding = "String DialogueManager will search using if statement"
->ending

== ending ===
* Close
~closeTriggered = true
->END