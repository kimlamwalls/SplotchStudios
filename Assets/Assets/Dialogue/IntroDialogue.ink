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
        -> text4

=== text3 ===
A pendant sits in your hand, a strange symbol in yellow.
~scriptEnding = "String DialogueManager will search for this string using if statement"
->ending

=== text4 ===
A feeling of safety, and perhaps, conviction. 
->ending

== ending ===
* Close
~closeTriggered = true
->END