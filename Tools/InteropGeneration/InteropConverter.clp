(defrule first-rule
         "Loads the input file"
         (initial-fact)
         =>
         (if (open "InteropInput.in" file "r") then
           (assert (Read line from file))))

(defrule read-line-from-file
         (declare (salience 4))
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
         (declare (salience 3))
         ?f <- (Line Macro ?name "(" $?args ")" ?body)
         =>
         (retract ?f)
         (assert (Macro Header ?name "(" $?args ")")
                 (Macro Implementation ?name "(" $?args ")")
                 (Macro C# ?name "(" $?args ")")))

(defrule generate-interop-constant-code-int
         (declare (salience 3))
         ?f <- (Line Constant ?name ?value)
         (test (integerp ?value))
         =>
         (retract ?f)
         (assert (Constant Header ?name int)
                 (Constant Implementation ?name int)
                 (Constant C# ?name int)))

(defrule generate-interop-constant-code-string
         (declare (salience 3))
         ?f <- (Line Constant ?name ?value)
         (test (stringp ?value))
         =>
         (retract ?f)
         (assert (Constant Header ?name char*)
                 (Constant Implementation ?name char*)
                 (Constant C# ?name IntPtr)))

(defrule generate-interop-macro-header
         (declare (salience 2))
         ?f <- (Macro Header ?name "(" $?args ")")
         =>
         (retract ?f)
         (format t "extern ? Interop_%s(%s);%n" ?name (implode$ $?args)))

(defrule generate-interop-constant-header
         (declare (salience 2))
         ?f <- (Constant Header ?name ?retType) 
         =>
         (retract ?f)
         (format t "extern %s Interop_Get%s();%n" ?name ?retType))

(defrule generate-interop-macro-implementation
         (declare (salience 1))
         ?f <- (Macro Implementation ?name "(" $?args ")")
         =>
         (retract ?f)
         (bind ?ia (implode$ $?args))
         (format t "? Interop_%s(%s) {%nreturn %s(%s);%n}%n" ?name ?ia ?name ?ia))

(defrule generate-interop-constant-implementation
         (declare (salience 1))
         ?f <- (Constant Implementation ?name ?retType) 
         =>
         (retract ?f)
         (format t "%s Interop_Get%s() {%nreturn %s;%n}%n " ?retType ?name ?name))

(defrule generate-interop-macro-c#
         ?f <- (Macro C# ?name "(" $?args ")")
         =>
         (retract ?f)
         (bind ?ia (implode$ $?args))
         (format t "[DllImport(\"libclips.so\")]%nprivate static extern ? Interop_%s(%s);%n" ?name ?ia))

(defrule generate-interop-constant-c#-int
         ?f <- (Constant C# ?name int) 
         =>
         (retract ?f)
         (format t "[DllImport(\"libclips.so\")]%nprivate static extern int Interop_Get%s();%n" ?name))

(defrule generate-interop-constant-c#-IntPtr
         ?f <- (Constant C# ?name IntPtr) 
         =>
         (retract ?f)
         (format t "[DllImport(\"libclips.so\")]%nprivate static extern IntPtr Interop_Get%s();%n" ?name))

(defrule generate-interop-constant-c#
         ?f <- (Constant C# ?name ?retType ) 
         =>
         (retract ?f)
         (format t "[DllImport(\"libclips.so\")]%nprivate static extern %s Interop_Get%s();%n" ?retType ?name))
