package com.lwn.lwntest.normal;

import java.util.Random;

public class LwnTestUtil {
	
	
	public static String randomString(int seed) {
        Random rand = new Random(seed);
        StringBuilder sb = new StringBuilder();
        while(true) {
            int n = rand.nextInt(27);
            if (n == 0) break;
            sb.append((char) ('`' + n));
        }
        return sb.toString();
    }


}
