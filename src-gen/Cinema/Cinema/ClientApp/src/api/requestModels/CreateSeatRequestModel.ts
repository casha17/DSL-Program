import {NightPlan} from "../models/schedules/NightPlan"
export type CreateSeatRequestModel = {
	name: string
	weight: number
	nightPlans: NightPlan[]
} 
