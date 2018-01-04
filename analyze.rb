require 'csv'

require 'active_support/all'

max = 500000

all_trans = []
CSV.foreach("./data.csv") do |row|
  all_trans << {
      no: row[0],
      size: row[1].to_f,
      fee: row[2].to_f
  }
end

result_array = []
sorted_no_array = []
all_trans.permutation {|data_array|
  p data_array
  total = 0.0
  no_array = []
  data_array.length.times do |i|
    linehash = data_array[i]
    tmptotal = total + linehash[:size]
    if tmptotal < max
      total = tmptotal
      no_array << linehash[:no]
    else
      break
    end
  end
  if sorted_no_array.index( no_array.sort ).nil?
    sorted_no_array << no_array.sort
    result_array << {
        total: total,
        no_array: no_array
    }
  end
}

result_pattern = result_array.sort{|a,b| a[:total] <=> b[:total] }.reverse[0]
p result_pattern
total_fee = 0.0
result_pattern[:no_array].each do |no|
  tran = all_trans.detect{|item| item[:no] == no }
  total_fee += tran[:fee]
end
p "total_fee: #{total_fee} + 12.5"