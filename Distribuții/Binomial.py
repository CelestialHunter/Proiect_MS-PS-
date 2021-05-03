from random import random
import numpy as np

class Bin:

    def __init__(self, *params):
        self.X, self.p = np.split(np.array(params), [len(params) - 1])
        self.q = 1 - self.p
        self.n = len(self.X)

    def simulate(self):
        probs_of_trials = np.zeros(self.n)
        comb = p = 1
        n = self.n - 1
        probs_of_trials[0] = q = self.q**n
        for i in range(1, self.n):
            comb *= (n - i + 1)/i
            p*=self.p
            q/=self.q
            probs_of_trials[i] = comb * p * q

        k = 0
        F = probs_of_trials[0]
        u = random()
        while u >= F:
            k = k + 1
            F = F + probs_of_trials[k]

        return int(self.X[k])

