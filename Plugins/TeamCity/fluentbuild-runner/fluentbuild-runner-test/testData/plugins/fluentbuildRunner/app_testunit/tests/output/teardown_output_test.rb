# @author: Roman Chernyatchik
require "test/unit"

class TeardownOutputTest < Test::Unit::TestCase

  def teardown
    $stdout << "teardown:$stdout<<msg1"
    STDOUT << "\nteardown:STDOUT<<msg2\n"
    $stderr << "teardown:$stderr<<msg3\n"
    STDERR << "teardown:STDERR<<msg4\n"
  end

  def test_fake
    assert true
  end
end