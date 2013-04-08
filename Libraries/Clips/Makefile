program: native
	dmcs -t:library -out:Libraries.Clips.dll *.cs

clean: native-clean 
	rm -f Libraries.Clips.dll 

native:
	$(MAKE) -C native

native-clean: 
	$(MAKE) -C native clean
