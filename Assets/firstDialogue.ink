EXTERNAL addQuest(questName)

Hello there! 
-> main

=== main ===
How are you feeling today?
+ [Happy]
    That makes me feel <color=\#F8FF30>happy</color> as well! 
+ [Sad]
    Oh, well that makes me <color=\#5B81FF>sad</color> too. 
    
- Anyways, would you like to accept my quest to destroy the bandit base north our vilage?
+ [Yes]
   //add a quest to their quest manager
   ~addQuest("Clear Bandit Base")
   -> DONE
+[No]
  //do nothing
-> END