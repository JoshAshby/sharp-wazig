const std = @import("std");
const testing = std.testing;

extern fn hello() void;
extern fn print(str_ptr: [*]const u8) void;

export fn add(a: i32, b: i32) i32 {
    hello();
    print("Hia from Zig in Wasm land");

    return a + b;
}

test "basic add functionality" {
    testing.expect(add(3, 7) == 10);
}
