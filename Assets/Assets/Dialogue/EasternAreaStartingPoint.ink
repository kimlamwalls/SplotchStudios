VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
Your ears catch skittering sounds in the distance.
    + [...]
        -> text1

=== text1 ===
You see a winged insectoid creature with a head covered in antennae.
        -> ending

=== ending ===
* Close
~closeTriggered = true
->END