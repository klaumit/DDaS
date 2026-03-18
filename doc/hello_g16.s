	.arch i8086,jumps
	.code16
	.att_syntax prefix
#NO_APP
	.section	.rodata
.LC0:
	.string	"Hello world"
	.text
	.global	main
	.type	main, @function
main:
	pushw	%bp
	movw	%sp,	%bp
	movw	$.LC0,	%ax
	pushw	%ax
	pushw	%ss
	popw	%ds
	call	puts
	addw	$2,	%sp
	movw	$0,	%ax
	pushw	%ax
	pushw	%ss
	popw	%ds
	call	exit
	.size	main, .-main
	.ident	"GCC: (GNU) 6.3.0"
