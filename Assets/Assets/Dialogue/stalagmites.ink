VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
A thick, unknown liquid drips between the stalactites and stalagmites before you. 
    + [...]
        -> text1

=== text1 ===
The tarry liquid moves and shifts, enticing you to touch it.
    + [Touch it]
        -> touch
    + [Step back]
        -> avoid

=== touch ===
The warm liquid moves to cover your hand and weapon. While you feel this will be beneficial, it feels unsettling at the same time.
[Attack range increases] [Sanity threshold slightly decreases]
~scriptEnding = "touch"
->ending

=== avoid ===
Coming to your senses, you step back and avoid the unknown.
->ending

== ending ===
* Close
~closeTriggered = true
->END