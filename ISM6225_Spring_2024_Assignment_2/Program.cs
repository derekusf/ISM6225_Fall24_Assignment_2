using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1: Find Missing Numbers in Array
            Console.WriteLine("Question 1:");
            int[] nums1 = { 4, 3, 2, 7, 8, 2, 3, 1 };
            IList<int> missingNumbers = FindMissingNumbers(nums1);
            Console.WriteLine(string.Join(",", missingNumbers));

            //Question 2: Sort Array by Parity
            Console.WriteLine("Question 2:");
            int[] nums2 = { 3, 1, 2, 4 };
            int[] sortedArray = SortArrayByParity(nums2);
            Console.WriteLine(string.Join(",", sortedArray));

            // Question 3: Two Sum
            Console.WriteLine("Question 3:");
            int[] nums3 = { 2, 7, 11, 15 };
            int target = 9;
            int[] indices = TwoSum(nums3, target);
            Console.WriteLine(string.Join(",", indices));

            // Question 4: Find Maximum Product of Three Numbers
            Console.WriteLine("Question 4:");
            int[] nums4 = { 1, 2, 3, 4 };
            int maxProduct = MaximumProduct(nums4);
            Console.WriteLine(maxProduct);

            // Question 5: Decimal to Binary Conversion
            Console.WriteLine("Question 5:");
            int decimalNumber = 42;
            string binary = DecimalToBinary(decimalNumber);
            Console.WriteLine(binary);

            // Question 6: Find Minimum in Rotated Sorted Array
            Console.WriteLine("Question 6:");
            int[] nums5 = { 3, 4, 5, 1, 2 };
            int minElement = FindMin(nums5);
            Console.WriteLine(minElement);

            // Question 7: Palindrome Number
            Console.WriteLine("Question 7:");
            int palindromeNumber = 121;
            bool isPalindrome = IsPalindrome(palindromeNumber);
            Console.WriteLine(isPalindrome);

            // Question 8: Fibonacci Number
            Console.WriteLine("Question 8:");
            int n = 4;
            int fibonacciNumber = Fibonacci(n);
            Console.WriteLine(fibonacciNumber);
        }

        // Question 1: Find Missing Numbers in Array
        public static IList<int> FindMissingNumbers(int[] nums)
        {
            try
            {
                // Write your code here
                List<int> missingItems = new List<int>();
                // Check the empty list
                if (nums.Length == 0){
                    return missingItems;
                }
                else{ //For positive cases
                    Array.Sort(nums);                   //sort the O(nlogn)
                    int j = 0;                          // Running index to keep track of assessed element
                    for(int i=1; i <= nums.Length; i++ ){ //traverse to all possible values
                        if (j >= nums.Length){ //have traversed all elements of nums
                          missingItems.Add(i);
                          continue;
                        }
                        for (int k = j; k < nums.Length; k++){ //traverse through all elements of nums
                            if(nums[k] < i){
                                //If element less than expected value, continue to find next item
                                //If reach the end of the list, add missing value
                                if (k == nums.Length-1){
                                    missingItems.Add(i);
                                }
                                j++;
                                continue;
                            }
                            else if (nums[k] == i){// found it
                                j++;
                                break;
                            } 
                            else if (nums[k] > i){// No value found aka missing
                                missingItems.Add(i);
                                break;
                            } 
                        }
                    }
                    return missingItems;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /*
            Initially, the time complexity was n^2. 
            To reduce time complexity to nlogn, I to pay more effort on array traversing
            There are trade off of "memory" vs time by adding parameter to track
            Traversing array is very error-proning, especially for edge cases. Lots of time spent for debugging
            Add watcher is very effective for debugging, especially with the above edge cases. 
        */

        // Question 2: Sort Array by Parity
        public static int[] SortArrayByParity(int[] nums)
        {
            try
            {
                // Write your code here
                int[] output = new int[nums.Length];
                if (nums.Length == 0){// Check the empty list
                    return new int[0];
                }
                int index = 0; 
                //Traverse through the nums, get all even integers
                for (int i = 0; i< nums.Length; i++){
                    if (nums[i] % 2 == 0){
                        output[index] = nums[i];
                        index ++;
                    }
                }
                //Traverse through the nums, get all odd integers
                for (int i = 0; i< nums.Length; i++){
                    if (nums[i] % 2 == 1){
                        output[index] = nums[i];
                        index ++;
                    }
                }

                return output; // Placeholder
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Question 3: Two Sum
        public static int[] TwoSum(int[] nums, int target)
        {
            try
            {
                // Write your code here
                int[] output = {-1,-1};
                if (nums.Length == 0){// Check the empty list
                    return new int[0];
                }
                //Traverse through the nums for the 1st factor
                for (int i = 0; i< nums.Length; i++){
                    //Traverse through the nums for the 2nd factor
                    for (int j = 0; j< nums.Length; j++){
                        if(i==j) continue; //skip if same element
                        if(nums[i] + nums[j] == target){ // found the factors
                            output[0] = i;
                            output[1] = j;
                            return output; 
                        }
                    }
                }

                return output; //if not found return {-1,-1}
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Question 4: Find Maximum Product of Three Numbers
        public static int MaximumProduct(int[] nums)
        {
            try
            {
                // Write your code here
                if (nums.Length == 0){// Check the empty list
                    return 0;
                }
                int maxp = 0;
                Array.Sort(nums); 
                //As max production of 3 are always the combination of 3 top positive elements
                // or 2 smallest negative elements * 1 largest positive element
                // or 3 top negative elements (if all are negative)
                // --> Get 2 smallest and 3 largest for assessment. 
                int[] assess_array; 
                if(nums.Length >= 5){
                    assess_array = new int[5];
                    assess_array[0] = nums[0]; 
                    assess_array[1] = nums[1]; 
                    assess_array[2] = nums[nums.Length-3]; 
                    assess_array[3] = nums[nums.Length-2]; 
                    assess_array[4] = nums[nums.Length-1]; 
                }
                else{
                    assess_array = nums;
                }
                //Traverse through the nums for the 1st factor 
                for (int i = 0; i< assess_array.Length; i++){ 
                    //Traverse through the nums for the 2nd factor
                    for (int j = i+1; j< assess_array.Length; j++){
                        //Traverse through the nums for the 3rd factor
                        for (int k = j+1; k <assess_array.Length; k++){
                            int prod = assess_array[i]*assess_array[j]*assess_array[k];
                            if (prod > maxp){
                                maxp = prod;
                            }
                        }
                    }
                }
                return maxp; 
            }
            catch (Exception)
            {
                throw;
            }
        }
        /*
            Again to reduce the time complexity, and handling negatives scenarios, it is necessary
            to spend time and effort for analysis
        */

        // Question 5: Decimal to Binary Conversion
        public static string DecimalToBinary(int decimalNumber)
        {
            try
            {
                // Write your code here
                //Using prebuilt function
                // string binary = Convert.ToString(decimalNumber, 2);

                //Manual build function
                int l = Convert.ToInt32(Math.Log2(Math.Abs(decimalNumber))) + 1; //calculate the length of binary number
                int[] digit = new int[l];
                int remain = Math.Abs(decimalNumber);
                string binary = "";
                for (int i = l-1; i >= 0 ; i--){
                    digit[i] = remain % 2;
                    remain = remain /2;
                    binary = digit[i] + binary;
                }
                //Assume we working on 16-bit binary. 
                //Handle negative number: 
                if (decimalNumber < 0){
                    binary = "";
                    for(int i = 0; i < 16 ; i ++){
                        if(l - i > 0){
                            binary = digit[i] + binary;
                        }
                        else{
                            binary = "1" + binary;
                        }
                    }
                }
                return binary; 
            }
            catch (Exception)
            {
                throw;
            }
        }
        /*
            Indeed there is prebuild C# functions, suggest to clarify if we need to build it manually. 
            And for negative number, it depends on # bit used. 
        */

        // Question 6: Find Minimum in Rotated Sorted Array
        public static int FindMin(int[] nums)
        {
            try
            {
                // Write your code here
                if (nums.Length == 0){// Check the empty list
                    return -99999;
                }
                int min = nums[0]; // init the value with 1st element;
                for(int i = 0; i < nums.Length; i++) 
                {
                    if(nums[i] < min){ //found the smaller one, as rotated sort, this is the smallest too. We can exit here.
                        min = nums[i];
                        break;
                    }
                }
                return min; 
            }
            catch (Exception)
            {
                throw;
            }
        }
        /*
            Rotated sorted array description wasn't clear, my assumption was that the sorted array (in ascending order) is split 
            and the 1st rotated ONCE to the back. Suggest to have clearer description
        */

        // Question 7: Palindrome Number
        public static bool IsPalindrome(int x)
        {
            try
            {
                // Write your code here
                string num = "";
                string rev_num = "";
                int digit = 0;
                int remainder = Math.Abs(x); 
                //Split digits from 
                while(remainder >0){
                    digit = remainder % 10;
                    remainder = remainder / 10;
                    num = digit + num;          //build string of right order number
                    rev_num = rev_num + digit;  //build string of reversed order number
                }
                if (num == rev_num) return true;
                else return false; 
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Question 8: Fibonacci Number
        public static int Fibonacci(int n)
        {
            try
            {
                // Write your code here
                //Out of range
                int fn = 0;
                if (n < 0 || n > 30){
                    return -1; // assume that we return -1 for out of range 
                }
                if(n == 0) return 0;
                else if(n ==1) return 1; 
                else{ //for n > 1
                    int fn_2 = 0;  //init f(n-2)
                    int fn_1 = 1;  //init f(n-1)
                    for (int i = 2; i <=n ; i++){ //calculate f(n)
                        fn = fn_2 + fn_1; //calculate F(n)
                        // Update f_2, f_1 for next cycle
                        fn_2 = fn_1;
                        fn_1 = fn;
                    }
                }
                return fn; 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
