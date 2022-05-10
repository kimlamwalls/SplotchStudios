VAR scriptEnding = 0
VAR closeTriggered = false

-> main

=== main ===
Your ears catch skittering sounds in the distance.
    + [...]
        -> text1

=== text1 ===
You see a winged insectoid creature with a head covered in antennae.
    + [...]
        -> text2

=== text2 === 
The creature turns to you, motions slightly in acknowldgement and then disappears into the darkness.
        -> ending

=== ending ===
* Close
~closeTriggered = true
->END