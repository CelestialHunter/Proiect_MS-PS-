from random import random
import numpy as np

class Bin:

    def __init__(self, *params):
        self.X, self.p = np.split(np.array(params), [len(params) - 1])
        self.q = 1 - self.p
        self.n = len(self.X)

    def simulate(self):
        prb_trials = np.zeros(self.n)
        n = self.n - 1
        prb_trials[0] = self.q**n

        for i in range(1, self.n):
            prb_trials[i] = prb_trials[i - 1] * self.p / self.q * (n - i + 1)/i

        k = 0
        F = prb_trials[0]
        u = random()
        while u >= F:
            k = k + 1
            F = F + prb_trials[k]

        return int(self.X[k])

