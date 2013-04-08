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

(defrule generate-interop-macro-code
 ?f <- (Line Macro ?name "(" $?args ")" ?body)
 =>
 (retract ?f)
 (assert (Macro Header ?name "(" $?args ")")
         (Macro Implementation ?name "(" $?args ")")
         (Macro C# ?name "(" $?args ")")))

(defrule generate-interop-constant-code
 ?f <- (Line Constant ?name ?value)
 =>
 (retract ?f)
 (assert (Constant Header ?name)
         (Constant Implementation ?name)
         (Constant C# ?name)))
