from random import random

class Bern:

    def __init__(self, p):
        self.p = p

    def simulate(self):
        u = random()
        return int(u < self.p)
