VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
Your vision unblurs, dark caverns unveil themselves before you.
    + [...]
        -> text1

=== text1 ===
A familiar place with untold secrets.
~scriptEnding = "text1"
    + [...]
        -> text2

=== text2 ===
Your mind is hazy as control returns. 
~scriptEnding = "text2"
    + [Have a look around]
        -> text3
    + [Stand there and do nothing]
        -> text3

=== text3 ===
A pendant sits in your hand, a strange symbol in yellow.
~scriptEnding = "text3"
->ending

== ending ===
* Close
~closeTriggered = true
->END