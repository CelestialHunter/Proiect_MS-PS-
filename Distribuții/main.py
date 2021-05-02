from Bernoulli import Bern
from Uniforma import Unif
from Geometric import Geom
from Exponential import Exp
from NonUniform import NonUnif
import sys

# types of distributions:
#   0 - Bernoulli -> one parameter, p (0 < p < 1)
#   1 - Uniform -> two parameters, a and b (interval endpoints, integers)
#   2 - Geometric -> one parameter, p (0 < p < 1)
#   3 - Exponential -> one parameter, theta (float, strictly positive)
#   4 - NonUniform -> more than three parameters: a and b (interval endpoints, integers) and p (the probability
#   for each number in the interval [a, b])
# command line arguments:  iterations, type_of_distribution, parameters

if __name__ == "__main__":
    if len(sys.argv) > 1:
        itr, type = [int(argc) for argc in sys.argv[1:3]]
        params = [float(argc) for argc in sys.argv[3:]]
        distrbutions = [Bern, Unif, Geom, Exp, NonUnif]

        distr = distrbutions[type](*params)
        for i in range(itr):
            print(distr.simulate())



        # FOR TESTING PURPOSES ( WILL DELETE SOON )
        #import numpy as np
        #X = distr.generate_interval()
        #Y = np.zeros(1000, dtype=int)

        #for i in range(itr):
            #Y[distr.simulate()]+=1

        #for x in X:
            #print(x, 'apare de:', Y[x], 'ori')
