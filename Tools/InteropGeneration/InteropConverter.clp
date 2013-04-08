(defrule first-rule
 "Loads the input file"
 (initial-fact)
 =>
 (if (open "InteropInput.in" file "r") then
 (assert (Read line from file))))

(defrule read-line-from-file
 ?f <- (Read line from file)
 =>
 (retract ?f)
 (bind ?result (readline file))
 (if (neq ?result EOF) then
     (assert (Line (explode$ ?result))
             (Read line from file))
     else
     (close file)))


 


 
