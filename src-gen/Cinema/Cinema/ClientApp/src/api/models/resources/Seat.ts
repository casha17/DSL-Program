import {NightPlan} from "../schedules/NightPlan"

export type Seat = {
	id: string
	name: string
	weight: number
	nightPlans: NightPlan[]
} 
